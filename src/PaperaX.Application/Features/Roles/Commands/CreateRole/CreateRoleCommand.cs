using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PaperaX.Application.Features.Roles.Queries.GetAllRoles;

namespace PaperaX.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? PerformedByUserId { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync(cancellationToken);

            var auditDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Code = role.Code,
                Description = role.Description,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt
            };

            var audit = new RoleAudit
            {
                RoleId = role.Id,
                Action = "CREATE",
                NewValue = JsonSerializer.Serialize(auditDto),
                PerformedBy = request.PerformedByUserId,
                Timestamp = DateTime.UtcNow
            };

            _context.RoleAudits.Add(audit);
            await _context.SaveChangesAsync(cancellationToken);

            return role.Id;
        }
    }
}
