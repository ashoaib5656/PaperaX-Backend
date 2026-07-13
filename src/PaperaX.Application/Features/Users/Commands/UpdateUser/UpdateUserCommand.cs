using MediatR;
using PaperaX.Shared.DTOs.Users;

namespace PaperaX.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
        public UpdateAccountSettingsRequest Request { get; set; }

        public UpdateUserCommand(int userId, UpdateAccountSettingsRequest request)
        {
            UserId = userId;
            Request = request;
        }
    }
}
