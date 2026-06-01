using System;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using PaperaX.Application.Features.Auth.Interfaces;

namespace PaperaX.Infrastructure.Authentication
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly string _clientId;

        public GoogleAuthService(IConfiguration configuration)
        {
            _clientId = configuration["GoogleAuthSettings:ClientId"] ?? throw new ArgumentNullException("Google ClientId is missing.");
        }

        public async Task<(string Email, string FullName, string GoogleId)?> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { _clientId }
                };

                // This line cryptographically verifies the token with Google's servers
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

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
