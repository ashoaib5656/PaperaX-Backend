using PaperaX.Domain.Entities;

namespace PaperaX.Application.Features.Auth.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
