using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>> { }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime? UpdatedAt { get; set; }
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllRolesQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Select(r => new RoleDto { 
                    Id = r.Id, 
                    Name = r.Name, 
                    Code = r.Code,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
