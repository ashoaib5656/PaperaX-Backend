using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public UpdateMenuCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<bool> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            
            if (menu == null)
            {
                return false;
            }

            if (!request.IsEnabled && menu.IsEnabled)
            {
                bool hasEnabledChild = await _context.Menus.AnyAsync(m => m.ParentId == menu.Id && m.IsEnabled && m.Id != menu.Id, cancellationToken);
                if (hasEnabledChild)
                {
                    throw new System.InvalidOperationException("Cannot disable a parent menu that has enabled child menus.");
                }
            }

            var oldValue = JsonSerializer.Serialize(menu);

            menu.Title = request.Title;
            menu.Code = request.Code;
            menu.Route = request.Route;
            menu.Icon = request.Icon;
            menu.ParentId = request.ParentId;
            menu.OrderNo = request.OrderNo;
            menu.Description = request.Description;
            menu.IsVisible = request.IsVisible;
            menu.IsEnabled = request.IsEnabled;
            menu.Placement = request.Placement;
            menu.PermissionId = request.PermissionId;
            menu.FeaturedTitle = request.FeaturedTitle;
            menu.FeaturedDescription = request.FeaturedDescription;
            menu.FeaturedRoute = request.FeaturedRoute;
            menu.FeaturedLinkText = request.FeaturedLinkText;
            menu.UpdatedAt = DateTime.UtcNow;

            _context.Menus.Update(menu);
            
            var existingRoles = await _context.MenuRoles.Where(mr => mr.MenuId == menu.Id).ToListAsync(cancellationToken);
            _context.MenuRoles.RemoveRange(existingRoles);
            
            if (request.RoleIds != null && request.RoleIds.Any())
            {
                var menuRoles = request.RoleIds.Select(roleId => new MenuRole
                {
                    MenuId = menu.Id,
                    RoleId = roleId
                });
                _context.MenuRoles.AddRange(menuRoles);
            }
            
            await _context.SaveChangesAsync(cancellationToken);

            var audit = new MenuAudit
            {
                MenuId = menu.Id,
                Action = "UPDATE",
                OldValue = oldValue,
                NewValue = JsonSerializer.Serialize(menu),
                PerformedBy = request.PerformedByUserId,
                Timestamp = DateTime.UtcNow
            };
            _context.MenuAudits.Add(audit);
            await _context.SaveChangesAsync(cancellationToken);

            await InvalidateCache(request.Placement);

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
