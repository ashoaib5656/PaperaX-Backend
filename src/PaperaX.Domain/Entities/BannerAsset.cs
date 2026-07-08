using System;

namespace PaperaX.Domain.Entities
{
    public class BannerAsset
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; } = null!;

        public string? DesktopImageUrl { get; set; } // e.g., 1920x600
        public string? TabletImageUrl { get; set; }  // e.g., 1024x768
        public string? MobileImageUrl { get; set; }  // e.g., 768x1024
        
        // Optional video/animation features for the future
        public string? VideoUrl { get; set; }
    }
}
