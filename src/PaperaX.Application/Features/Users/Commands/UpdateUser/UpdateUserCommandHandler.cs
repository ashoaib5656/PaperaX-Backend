using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using PaperaX.Application.Interfaces;
using PaperaX.Shared.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with id {command.UserId} not found.");
            }

            user.FullName = command.Request.FullName;
            user.Phone = command.Request.Phone;
            user.Address = command.Request.Address;
            
            if (command.Request.Company != null)
                user.Company = command.Request.Company;
                
            if (command.Request.Gstin != null)
                user.Gstin = command.Request.Gstin;

            await _context.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Company = user.Company,
                Address = user.Address,
                Gstin = user.Gstin,
                Status = user.Status,
                HasLocalPassword = !string.IsNullOrEmpty(user.PasswordHash),
                IsDeleted = user.IsDeleted
            };
        }
    }
}
