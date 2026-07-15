using MediatR;
using PaperaX.Domain.Enums;

namespace PaperaX.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommand : IRequest<bool>
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
        public MenuPlacement Placement { get; set; }
        
        public int? PermissionId { get; set; }

        public string? FeaturedTitle { get; set; }
        public string? FeaturedDescription { get; set; }
        public string? FeaturedRoute { get; set; }
        public string? FeaturedLinkText { get; set; }

        public int? PerformedByUserId { get; set; }

        public System.Collections.Generic.List<int> RoleIds { get; set; } = new();
    }
}
