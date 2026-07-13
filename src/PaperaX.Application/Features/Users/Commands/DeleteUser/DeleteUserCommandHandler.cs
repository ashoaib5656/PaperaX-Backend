using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with id {command.UserId} not found.");
            }

            user.IsDeleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
