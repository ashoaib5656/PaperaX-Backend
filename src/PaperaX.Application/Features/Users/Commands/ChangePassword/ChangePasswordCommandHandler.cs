using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using PaperaX.Application.Interfaces;

namespace PaperaX.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ChangePasswordCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with id {command.UserId} not found.");
            }

            if (string.IsNullOrEmpty(command.Request.CurrentPassword))
            {
                throw new Exception("Current password is required.");
            }

            if (string.IsNullOrEmpty(command.Request.NewPassword))
            {
                throw new Exception("New password is required.");
            }

            if (!string.IsNullOrEmpty(user.PasswordHash) && !BCrypt.Net.BCrypt.Verify(command.Request.CurrentPassword, user.PasswordHash))
            {
                throw new Exception("Incorrect current password.");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Request.NewPassword);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
