using System;

namespace PaperaX.Application.Features.Banners.DTOs
{
    public class BannerDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string BannerType { get; set; } = "Static";
        public string TargetAudience { get; set; } = "All";
        public string Placement { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; } = string.Empty;
        
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? CtaText { get; set; }
        public string? CtaLink { get; set; }
        public string? ButtonStyle { get; set; }
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public int Priority { get; set; }
        
        public int? PromotionId { get; set; }

        // Asset fields
        public string? DesktopImageUrl { get; set; }
        public string? TabletImageUrl { get; set; }
        public string? MobileImageUrl { get; set; }
        
        // Targeting fields
        public decimal? MinCartValue { get; set; }
        public string? DeviceTarget { get; set; }
        public string? CountryTarget { get; set; }
        public int? MaxViewsPerUser { get; set; }
        
        // A/B Testing
        public string? VariantGroup { get; set; }
    }
}
