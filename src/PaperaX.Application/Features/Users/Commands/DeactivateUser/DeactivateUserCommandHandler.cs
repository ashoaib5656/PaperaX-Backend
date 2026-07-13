using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Users.Commands.DeactivateUser
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeactivateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with id {command.UserId} not found.");
            }

            user.Status = "Inactive";

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
