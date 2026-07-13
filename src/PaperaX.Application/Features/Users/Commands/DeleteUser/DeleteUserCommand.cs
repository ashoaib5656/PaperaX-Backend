using MediatR;

namespace PaperaX.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
