using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using PaperaX.Application.Interfaces;
using PaperaX.Shared.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCurrentUserQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new Exception($"User with id {request.UserId} not found.");
            }

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
