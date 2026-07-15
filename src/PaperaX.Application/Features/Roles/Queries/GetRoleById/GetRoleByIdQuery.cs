using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Application.Features.Roles.Queries.GetAllRoles;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<RoleDto?>
    {
        public int Id { get; set; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetRoleByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(r => r.Id == request.Id)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Code = r.Code,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
