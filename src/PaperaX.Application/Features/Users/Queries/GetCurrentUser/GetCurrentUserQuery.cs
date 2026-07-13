using MediatR;
using PaperaX.Shared.DTOs.Users;

namespace PaperaX.Application.Features.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }

        public GetCurrentUserQuery(int userId)
        {
            UserId = userId;
        }
    }
}
