using MediatR;
using PaperaX.Shared.DTOs.Users;

namespace PaperaX.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public ChangePasswordRequest Request { get; set; }

        public ChangePasswordCommand(int userId, ChangePasswordRequest request)
        {
            UserId = userId;
            Request = request;
        }
    }
}
