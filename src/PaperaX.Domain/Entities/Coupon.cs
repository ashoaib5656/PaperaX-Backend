using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Domain.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty; 
        public decimal DiscountValue { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; } = true;
        public int? TotalUsageLimit { get; set; }
        public int CurrentUsageCount { get; set; } = 0;
        public int? LimitPerUser { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public bool FirstTimeOnly { get; set; } = false;
        public string? ApplicableCategories { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
