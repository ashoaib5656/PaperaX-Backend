using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
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
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly OtpRedisService _otpRedisService;

        public AuthService(
            IGoogleAuthService googleAuthService,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            IEmailService emailService,
            OtpRedisService otpRedisService)
        {
            _googleAuthService = googleAuthService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _emailService = emailService;
            _otpRedisService = otpRedisService;
        }

        public async Task SendOtpAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is required.", nameof(email));
            }

            // Generate a secure 6-digit numeric OTP
            var otp = GenerateSecureOtp();

            // Store in Redis (expires in 5 minutes)
            await _otpRedisService.StoreOtpAsync(email, otp);

            // Send via SMTP Email Service
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

            // Consume/remove OTP after successful verification
            await _otpRedisService.RemoveOtpAsync(email);
            return true;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            // Hash password with BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = "User",
                IsEmailVerified = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            // Generate authentication tokens
            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            // Save Refresh Token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
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

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Verify password using BCrypt
            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isValidPassword)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // Generate authentication tokens
            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            // Save Refresh Token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role ?? "User",
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

            var user = await _userRepository.GetByEmailAsync(googlePayload.Value.Email);

            if (user == null)
            {
                // Auto-register external Google login users
                user = new User
                {
                    Email = googlePayload.Value.Email,
                    FullName = googlePayload.Value.FullName,
                    GoogleId = googlePayload.Value.GoogleId,
                    IsEmailVerified = true,
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.AddAsync(user);
            }
            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                // Link Google authentication to existing email user
                user.GoogleId = googlePayload.Value.GoogleId;
                await _userRepository.UpdateAsync(user);
            }

            // Generate authentication tokens
            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            // Save Refresh Token
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role ?? "User",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new InvalidOperationException("User not found.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userRepository.UpdateAsync(user);

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateAsync(user);

            return new AuthResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role ?? "User",
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateSecureOtp()
        {
            // Secure numeric generator
            var randomNumber = new byte[4];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var value = BitConverter.ToUInt32(randomNumber, 0) % 900000 + 100000;
            return value.ToString();
        }
    }
}
