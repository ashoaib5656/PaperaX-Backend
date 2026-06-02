using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            var successResponse = ApiResponse<AuthResponse>.Success(response, "Login successful");
            return Ok(successResponse);
        }
    }
}
