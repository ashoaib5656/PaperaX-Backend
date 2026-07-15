using MediatR;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Menus.Queries.GetAllMenus
{
    public class GetAllMenusQuery : IRequest<List<MenuDetailsDto>>
    {
    }

    public class MenuDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public int OrderNo { get; set; }
        public string? Description { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public PaperaX.Domain.Enums.MenuPlacement Placement { get; set; }
        
        public int? PermissionId { get; set; }

        public string? FeaturedTitle { get; set; }
        public string? FeaturedDescription { get; set; }
        public string? FeaturedRoute { get; set; }
        public string? FeaturedLinkText { get; set; }

        public List<int> RoleIds { get; set; } = new();
    }
}
