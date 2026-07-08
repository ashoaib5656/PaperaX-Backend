using System;

namespace PaperaX.Domain.Entities
{
    public class BannerTargetingRule
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; } = null!;

        public decimal? MinCartValue { get; set; }
        public string? DeviceTarget { get; set; } // e.g., "All", "DesktopOnly", "MobileOnly"
        public string? CountryTarget { get; set; } // e.g., "US", "IN"
        
        public int? MaxViewsPerUser { get; set; } // Frequency capping
    }
}
