using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Menus.Queries.GetAllMenus
{
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<MenuDetailsDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllMenusQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDetailsDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var menus = await _context.Menus
                .Include(m => m.MenuRoles)
                .OrderBy(m => m.OrderNo)
                .ToListAsync(cancellationToken);

            return menus.Select(m => new MenuDetailsDto
            {
                Id = m.Id,
                Title = m.Title,
                Code = m.Code,
                Route = m.Route,
                Icon = m.Icon,
                ParentId = m.ParentId,
                OrderNo = m.OrderNo,
                Description = m.Description,
                IsVisible = m.IsVisible,
                IsEnabled = m.IsEnabled,
                Placement = m.Placement,
                PermissionId = m.PermissionId,
                FeaturedTitle = m.FeaturedTitle,
                FeaturedDescription = m.FeaturedDescription,
                FeaturedRoute = m.FeaturedRoute,
                FeaturedLinkText = m.FeaturedLinkText,
                RoleIds = m.MenuRoles.Select(mr => mr.RoleId).ToList()
            }).ToList();
        }
    }
}
