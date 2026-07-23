using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PaperaX.Api.Helpers;
using PaperaX.Application.Common.Models;
using PaperaX.Application.Features.Auth.DTOs;
using PaperaX.Application.Features.Auth.Interfaces;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _env;

        public AuthController(IAuthService authService, IWebHostEnvironment env)
        {
            _authService = authService;
            _env = env;
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.IdToken))
            {
                var errorResponse = ApiResponse<AuthResponse>.Failure("Google Token is required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            var response = await _authService.GoogleLoginAsync(request.IdToken);
            CookieHelper.SetRefreshTokenCookie(Response.HttpContext, _env, response.RefreshToken);
            response.RefreshToken = null; // Do not return in body
            var successResponse = ApiResponse<AuthResponse>.Success(response, "Login successful");
            return Ok(successResponse);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                var errorResponse = ApiResponse<string>.Failure("Email is required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            await _authService.SendOtpAsync(request.Email);
            var successResponse = ApiResponse<string>.Success(null, "OTP sent successfully");
            return Ok(successResponse);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
            {
                var errorResponse = ApiResponse<string>.Failure("Email and OTP are required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            var isValid = await _authService.VerifyOtpAsync(request.Email, request.Otp);
            if (isValid)
            {
                var successResponse = ApiResponse<string>.Success(null, "OTP verified successfully");
                return Ok(successResponse);
            }
            else
            {
                var errorResponse = ApiResponse<string>.Failure("Invalid OTP", "Verification failed");
                return Unauthorized(errorResponse);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                var errorResponse = ApiResponse<AuthResponse>.Failure("Email and Password are required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            var response = await _authService.RegisterAsync(request);
            CookieHelper.SetRefreshTokenCookie(Response.HttpContext, _env, response.RefreshToken);
            response.RefreshToken = null;
            var successResponse = ApiResponse<AuthResponse>.Success(response, "Registration successful");
            return Ok(successResponse);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                var errorResponse = ApiResponse<AuthResponse>.Failure("Email and Password are required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            var response = await _authService.ResetPasswordAsync(request);
            CookieHelper.SetRefreshTokenCookie(Response.HttpContext, _env, response.RefreshToken);
            response.RefreshToken = null;
            var successResponse = ApiResponse<AuthResponse>.Success(response, "Password reset successfully");
            return Ok(successResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                var errorResponse = ApiResponse<AuthResponse>.Failure("Email and Password are required", "Invalid Request");
                return BadRequest(errorResponse);
            }

            var response = await _authService.LoginAsync(request.Email, request.Password);
            CookieHelper.SetRefreshTokenCookie(Response.HttpContext, _env, response.RefreshToken);
            response.RefreshToken = null;
            var successResponse = ApiResponse<AuthResponse>.Success(response, "Login successful");
            return Ok(successResponse);
        }

        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(ApiResponse<bool>.Failure("Email is required", "Invalid Request"));
            }

            var exists = await _authService.CheckEmailExistsAsync(request.Email);
            return Ok(ApiResponse<bool>.Success(exists, "Email check completed"));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var rawRefreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(rawRefreshToken))
            {
                return Unauthorized(ApiResponse<string>.Failure("Refresh token missing", "Unauthorized"));
            }
            
            var refreshToken = Uri.UnescapeDataString(rawRefreshToken);

            try
            {
                var response = await _authService.RefreshTokenAsync(refreshToken);
                CookieHelper.SetRefreshTokenCookie(Response.HttpContext, _env, response.RefreshToken);
                response.RefreshToken = null; // Do not return in body
                return Ok(ApiResponse<AuthResponse>.Success(response, "Token refreshed successfully"));
            }
            catch (System.UnauthorizedAccessException ex)
            {
                CookieHelper.DeleteRefreshTokenCookie(Response.HttpContext, _env);
                return Unauthorized(ApiResponse<string>.Failure(ex.Message, "Unauthorized"));
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var rawRefreshToken = Request.Cookies["refresh_token"];
            if (!string.IsNullOrEmpty(rawRefreshToken))
            {
                var refreshToken = Uri.UnescapeDataString(rawRefreshToken);
                await _authService.LogoutAsync(refreshToken);
            }

            CookieHelper.DeleteRefreshTokenCookie(Response.HttpContext, _env);
            return Ok(ApiResponse<string>.Success(null, "Logged out successfully"));
        }
    }
}
