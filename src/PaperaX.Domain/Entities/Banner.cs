using System;
using System.Collections.Generic;

namespace PaperaX.Domain.Entities
{
    public class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public string Placement { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        // CMS Fields
        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public BannerAsset? Asset { get; set; }
        public BannerTargetingRule? TargetingRule { get; set; }
        public ICollection<BannerVersion> Versions { get; set; } = new List<BannerVersion>();
        public ICollection<BannerAnalytics> Analytics { get; set; } = new List<BannerAnalytics>();
        
        public string? VariantGroup { get; set; } // A/B testing tag
        
        public string BannerType { get; set; } = "Static";
        public string TargetAudience { get; set; } = "All";
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public string? CtaText { get; set; }
        public string? CtaLink { get; set; }
        public string? ButtonStyle { get; set; }
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public int Priority { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string Status 
        { 
            get 
            {
                if (!IsActive) return "Paused";
                var now = DateTime.UtcNow;
                if (now < StartDate) return "Scheduled";
                if (now > EndDate) return "Expired";
                return "Active";
            }
        }
    }
}
