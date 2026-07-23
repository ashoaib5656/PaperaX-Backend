using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Features.Auth.DTOs;
using PaperaX.Application.Features.Auth.Interfaces;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using PaperaX.Infrastructure.Redis;
using BCrypt.Net;

namespace PaperaX.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly OtpRedisService _otpRedisService;

        public AuthService(
            IApplicationDbContext context,
            IJwtTokenGenerator jwtTokenGenerator,
            IGoogleAuthService googleAuthService,
            IEmailService emailService,
            OtpRedisService otpRedisService)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _googleAuthService = googleAuthService;
            _emailService = emailService;
            _otpRedisService = otpRedisService;
        }

        private string GenerateSecureOtp()
        {
            byte[] randomNumber = new byte[4];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                int value = Math.Abs(BitConverter.ToInt32(randomNumber, 0));
                return (value % 1000000).ToString("D6");
            }
        }

        private static string HashToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private async Task<string> IssueRefreshTokenAsync(int userId)
        {
            var rawToken = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                TokenHash = HashToken(rawToken),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return rawToken;
        }

        private async Task RevokeAllForUserAsync(int userId)
        {
            var activeTokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.Revoked)
                .ToListAsync();

            foreach (var token in activeTokens)
            {
                token.Revoked = true;
            }
            await _context.SaveChangesAsync();
        }

        public async Task SendOtpAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }

            var otp = GenerateSecureOtp();
            await _otpRedisService.StoreOtpAsync(email, otp);
            await _emailService.SendOtpAsync(email, otp);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(otp))
            {
                return false;
            }

            var storedOtp = await _otpRedisService.GetOtpAsync(email);
            if (storedOtp == null || storedOtp != otp)
            {
                return false;
            }

            await _otpRedisService.RemoveOtpAsync(email);
            return true;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                LegacyRole = "Customer",
                Status = "Active",
                IsEmailVerified = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = await IssueRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.LegacyRole,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email and password are required.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = await IssueRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.LegacyRole ?? "Customer",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponse> GoogleLoginAsync(string idToken)
        {
            if (string.IsNullOrWhiteSpace(idToken))
            {
                throw new ArgumentException("Google ID Token is required.", nameof(idToken));
            }

            var googlePayload = await _googleAuthService.ValidateGoogleTokenAsync(idToken);
            if (googlePayload == null)
            {
                throw new UnauthorizedAccessException("Invalid Google Token");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == googlePayload.Value.Email);

            if (user == null)
            {
                user = new User
                {
                    Email = googlePayload.Value.Email,
                    FullName = googlePayload.Value.FullName,
                    GoogleId = googlePayload.Value.GoogleId,
                    IsEmailVerified = true,
                    LegacyRole = "Customer",
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = googlePayload.Value.GoogleId;
                await _context.SaveChangesAsync();
            }

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = await IssueRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.LegacyRole ?? "Customer",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
                ?? throw new InvalidOperationException("User not found.");

            if (!string.IsNullOrEmpty(request.Token))
            {
                if (user.InviteToken != request.Token)
                {
                    throw new UnauthorizedAccessException("Invalid or expired invitation link.");
                }

                if (user.InviteTokenExpiry.HasValue && user.InviteTokenExpiry.Value < DateTime.UtcNow)
                {
                    throw new UnauthorizedAccessException("Invitation link has expired.");
                }

                user.InviteToken = null;
                user.InviteTokenExpiry = null;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            if (user.Status == "Pending Setup")
            {
                user.Status = "Active";
            }

            await _context.SaveChangesAsync();

            await RevokeAllForUserAsync(user.Id);

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = await IssueRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.LegacyRole ?? "Customer",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        public async Task<AuthResponse> RefreshTokenAsync(string rawRefreshToken)
        {
            if (string.IsNullOrWhiteSpace(rawRefreshToken))
                throw new UnauthorizedAccessException("Refresh token is required.");

            var hashedToken = HashToken(rawRefreshToken);
            var storedToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == hashedToken);

            if (storedToken == null)
                throw new UnauthorizedAccessException("Invalid refresh token.");

            if (storedToken.Revoked)
            {
                await RevokeAllForUserAsync(storedToken.UserId);
                throw new UnauthorizedAccessException("Token reuse detected — all sessions revoked.");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token has expired.");

            var newRawRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            storedToken.Revoked = true;
            storedToken.ReplacedByTokenHash = HashToken(newRawRefreshToken);

            var newRefreshToken = new RefreshToken
            {
                TokenHash = HashToken(newRawRefreshToken),
                UserId = storedToken.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            var user = storedToken.User;
            var accessToken = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.LegacyRole ?? "Customer",
                AccessToken = accessToken,
                RefreshToken = newRawRefreshToken
            };
        }

        public async Task LogoutAsync(string rawRefreshToken)
        {
            if (string.IsNullOrWhiteSpace(rawRefreshToken))
                return;

            var hashedToken = HashToken(rawRefreshToken);
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.TokenHash == hashedToken);

            if (storedToken != null)
            {
                storedToken.Revoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
