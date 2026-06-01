using System.Threading.Tasks;

namespace PaperaX.Application.Features.Auth.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<(string Email, string FullName, string GoogleId)?> ValidateGoogleTokenAsync(string idToken);
    }
}
