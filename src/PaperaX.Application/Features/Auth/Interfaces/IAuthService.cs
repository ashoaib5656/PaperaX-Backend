using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaperaX.Application.Features.Auth.DTOs;

namespace PaperaX.Application.Features.Auth.Interfaces
{
    public interface IAuthService
    {
        Task SendOtpAsync(string email);
        Task<bool> VerifyOtpAsync(string email, string otp);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(string email, string password);
        Task<AuthResponse> GoogleLoginAsync(string idToken);
        Task<AuthResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
