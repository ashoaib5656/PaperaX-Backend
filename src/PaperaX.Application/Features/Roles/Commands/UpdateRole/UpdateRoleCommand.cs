using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PaperaX.Application.Features.Roles.Queries.GetAllRoles;

namespace PaperaX.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? PerformedByUserId { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateRoleCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            
            if (role == null)
            {
                return false;
            }

            if (!request.IsActive && role.IsActive)
            {
                var codeUpper = role.Code.ToUpper();
                if (codeUpper == "ADMIN" || codeUpper == "CUSTOMER" || codeUpper == "GUEST")
                {
                    throw new System.InvalidOperationException($"System roles ({role.Code}) cannot be deactivated.");
                }
            }

            var oldAuditDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Code = role.Code,
                Description = role.Description,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };
            var oldValue = JsonSerializer.Serialize(oldAuditDto);

            role.Description = request.Description;
            role.IsActive = request.IsActive;
            role.UpdatedAt = DateTime.UtcNow;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);

            var newAuditDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Code = role.Code,
                Description = role.Description,
                IsActive = role.IsActive,
                CreatedAt = role.CreatedAt,
                UpdatedAt = role.UpdatedAt
            };

            var audit = new RoleAudit
            {
                RoleId = role.Id,
                Action = "UPDATE",
                OldValue = oldValue,
                NewValue = JsonSerializer.Serialize(newAuditDto),
                PerformedBy = request.PerformedByUserId,
                Timestamp = DateTime.UtcNow
            };

            _context.RoleAudits.Add(audit);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate cache if there are UI elements dependent on role status
            await _cacheService.RemoveAsync($"menu:tree:v2:{role.Id}:Sidebar");
            await _cacheService.RemoveAsync($"menu:tree:v2:{role.Id}:TopNavbar");
            await _cacheService.RemoveAsync($"menu:tree:v2:{role.Id}:Footer");

            return true;
        }
    }
}
