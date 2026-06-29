using System;
using MediatR;

namespace PaperaX.Application.Features.Promotions.Commands.UpdatePromotion
{
    public class UpdatePromotionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string PromotionType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AppliesTo { get; set; } = string.Empty;
        public string? ApplicableCategories { get; set; }
        public string? ApplicableProducts { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public int? Priority { get; set; }
        public string? BannerImage { get; set; }
        public string? Description { get; set; }
    }
}
