using MediatR;
using PaperaX.Domain.Enums;

namespace PaperaX.Application.Features.Menus.Commands.CreateMenu
{
    public class CreateMenuCommand : IRequest<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public int OrderNo { get; set; }
        public string? Description { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public MenuPlacement Placement { get; set; }
        
        public int? PermissionId { get; set; }

        public string? FeaturedTitle { get; set; }
        public string? FeaturedDescription { get; set; }
        public string? FeaturedRoute { get; set; }
        public string? FeaturedLinkText { get; set; }

        // Optionally, who is performing this
        public int? PerformedByUserId { get; set; }

        // Role assignment
        public System.Collections.Generic.List<int> RoleIds { get; set; } = new();
    }
}
