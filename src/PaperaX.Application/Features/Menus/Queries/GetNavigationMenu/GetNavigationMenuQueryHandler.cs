using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Application.Features.Menus.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Menus.Queries.GetNavigationMenu
{
    public class GetNavigationMenuQueryHandler : IRequestHandler<GetNavigationMenuQuery, List<MenuItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public GetNavigationMenuQueryHandler(IApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<MenuItemDto>> Handle(GetNavigationMenuQuery request, CancellationToken cancellationToken)
        {
            int? roleId = null;
            string roleKey = "Guest";

            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                var guestRole = await _context.Roles.FirstOrDefaultAsync(r => r.Code == "GUEST", cancellationToken);
                if (guestRole != null)
                {
                    roleId = guestRole.Id;
                }
            }
            else
            {
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == request.RoleName.ToLower() || r.Code.ToLower() == request.RoleName.ToLower(), cancellationToken);
                if (role != null)
                {
                    roleId = role.Id;
                    roleKey = roleId.ToString()!;
                }
            }

            string cacheKey = $"menu:tree:v2:{roleKey}:{request.Placement}";
            var cachedMenus = await _cacheService.GetAsync<List<MenuItemDto>>(cacheKey);
            
            if (cachedMenus != null)
            {
                return cachedMenus;
            }

            var menusQuery = _context.Menus
                .Include(m => m.Children)
                .Where(m => m.Placement == request.Placement && m.IsVisible && m.IsEnabled);

            if (roleId != null)
            {
                menusQuery = menusQuery.Where(m => m.MenuRoles.Any(mr => mr.RoleId == roleId));
            }
            else
            {
                menusQuery = menusQuery.Where(m => false); // If no role matched, return nothing
            }

            var allMenus = await menusQuery
                .OrderBy(m => m.OrderNo)
                .ToListAsync(cancellationToken);

            // Filter out menus that require a permission the role doesn't have
            if (roleId != null)
            {
                var rolePermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Select(rp => rp.PermissionId)
                    .ToListAsync(cancellationToken);

                allMenus = allMenus.Where(m => m.PermissionId == null || rolePermissions.Contains(m.PermissionId.Value)).ToList();
            }

            var menuTree = BuildMenuTree(allMenus, null);

            await _cacheService.SetAsync(cacheKey, menuTree);

            return menuTree;
        }

        private List<MenuItemDto> BuildMenuTree(List<Domain.Entities.Menu> allMenus, int? parentId)
        {
            return allMenus
                .Where(m => m.ParentId == parentId)
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Route = m.Route,
                    Icon = m.Icon,
                    Description = m.Description,
                    FeaturedTitle = m.FeaturedTitle,
                    FeaturedDescription = m.FeaturedDescription,
                    FeaturedRoute = m.FeaturedRoute,
                    FeaturedLinkText = m.FeaturedLinkText,
                    Children = BuildMenuTree(allMenus, m.Id)
                })
                .ToList();
        }
    }
}
