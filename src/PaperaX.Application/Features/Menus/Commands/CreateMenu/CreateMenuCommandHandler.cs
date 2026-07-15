using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public CreateMenuCommandHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new Menu
            {
                Title = request.Title,
                Code = request.Code,
                Route = request.Route,
                Icon = request.Icon,
                ParentId = request.ParentId,
                OrderNo = request.OrderNo,
                Description = request.Description,
                IsVisible = request.IsVisible,
                IsEnabled = request.IsEnabled,
                Placement = request.Placement,
                PermissionId = request.PermissionId,
                FeaturedTitle = request.FeaturedTitle,
                FeaturedDescription = request.FeaturedDescription,
                FeaturedRoute = request.FeaturedRoute,
                FeaturedLinkText = request.FeaturedLinkText,
                CreatedAt = DateTime.UtcNow
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.RoleIds != null && request.RoleIds.Any())
            {
                var menuRoles = request.RoleIds.Select(roleId => new MenuRole
                {
                    MenuId = menu.Id,
                    RoleId = roleId
                });
                _context.MenuRoles.AddRange(menuRoles);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // Audit Logging
            var audit = new MenuAudit
            {
                MenuId = menu.Id,
                Action = "CREATE",
                NewValue = JsonSerializer.Serialize(menu),
                PerformedBy = request.PerformedByUserId,
                Timestamp = DateTime.UtcNow
            };
            _context.MenuAudits.Add(audit);
            await _context.SaveChangesAsync(cancellationToken);

            // Invalidate Caches for this placement
            await InvalidateCache(request.Placement);

            return menu.Id;
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
