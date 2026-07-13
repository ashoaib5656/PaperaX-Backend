using MediatR;

namespace PaperaX.Application.Features.Users.Commands.DeactivateUser
{
    public class DeactivateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public DeactivateUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
