using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public DeleteMenuCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.Include(m => m.MenuRoles).FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            
            if (menu == null)
            {
                return false;
            }

            bool hasChildren = await _context.Menus.AnyAsync(m => m.ParentId == menu.Id && (m.IsVisible || m.IsEnabled), cancellationToken);
            if (hasChildren)
            {
                throw new System.InvalidOperationException("Cannot delete a menu that has active child menus.");
            }

            var oldAuditDto = new PaperaX.Application.Features.Menus.Queries.GetAllMenus.MenuDetailsDto
            {
                Id = menu.Id,
                Title = menu.Title,
                Code = menu.Code,
                Route = menu.Route,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                OrderNo = menu.OrderNo,
                Description = menu.Description,
                IsVisible = menu.IsVisible,
                IsEnabled = menu.IsEnabled,
                Placement = menu.Placement,
                PermissionId = menu.PermissionId,
                FeaturedTitle = menu.FeaturedTitle,
                FeaturedDescription = menu.FeaturedDescription,
                FeaturedRoute = menu.FeaturedRoute,
                FeaturedLinkText = menu.FeaturedLinkText,
                RoleIds = menu.MenuRoles.Select(mr => mr.RoleId).ToList()
            };
            var oldValue = JsonSerializer.Serialize(oldAuditDto);
            var placement = menu.Placement;

            menu.IsVisible = false;
            menu.IsEnabled = false;
            menu.UpdatedAt = DateTime.UtcNow;

            _context.Menus.Update(menu);
            await _context.SaveChangesAsync(cancellationToken);

            var audit = new MenuAudit
            {
                MenuId = menu.Id, // Still store it even though it's deleted, or handle accordingly
                Action = "DELETE",
                OldValue = oldValue,
                PerformedBy = request.PerformedByUserId,
                Timestamp = DateTime.UtcNow
            };
            _context.MenuAudits.Add(audit);
            await _context.SaveChangesAsync(cancellationToken);

            await InvalidateCache(placement);

            return true;
        }

        private async Task InvalidateCache(Domain.Enums.MenuPlacement placement)
        {
            var roleIds = await _context.Roles.Select(r => r.Id).ToListAsync();
            foreach (var roleId in roleIds)
            {
                await _cacheService.RemoveAsync($"menu:tree:v2:{roleId}:{placement}");
            }
            await _cacheService.RemoveAsync($"menu:tree:v2:Guest:{placement}");
        }
    }
}
