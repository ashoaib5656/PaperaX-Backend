using System;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using PaperaX.Application.Features.Auth.Interfaces;

namespace PaperaX.Infrastructure.Authentication
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly GoogleAuthSettings _settings;

        public GoogleAuthService(IOptions<GoogleAuthSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<(string Email, string FullName, string GoogleId)?> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _settings.ClientId }
                };

                // This line cryptographically verifies the token with Google's servers
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

                return (payload.Email, payload.Name, payload.Subject);
            }
            catch (InvalidJwtException)
            {
                // Token is invalid or expired
                return null;
            }
        }
    }
}
