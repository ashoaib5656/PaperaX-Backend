using System;
using System.Collections.Generic;
using PaperaX.Domain.Enums;

namespace PaperaX.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
        public Menu? Parent { get; set; }
        public int OrderNo { get; set; }
        public string? Description { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public MenuPlacement Placement { get; set; }
        
        public int? PermissionId { get; set; }
        public Permission? Permission { get; set; }

        public string? FeaturedTitle { get; set; }
        public string? FeaturedDescription { get; set; }
        public string? FeaturedRoute { get; set; }
        public string? FeaturedLinkText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Menu> Children { get; set; } = new List<Menu>();
        public ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
    }
}
