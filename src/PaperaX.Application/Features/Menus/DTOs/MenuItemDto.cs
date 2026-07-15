using System.Collections.Generic;

namespace PaperaX.Application.Features.Menus.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Route { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        
        public string? FeaturedTitle { get; set; }
        public string? FeaturedDescription { get; set; }
        public string? FeaturedRoute { get; set; }
        public string? FeaturedLinkText { get; set; }

        public List<MenuItemDto> Children { get; set; } = new List<MenuItemDto>();
    }
}
