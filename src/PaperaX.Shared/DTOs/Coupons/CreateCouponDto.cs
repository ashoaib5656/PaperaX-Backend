using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Shared.DTOs.Coupons
{
    public class CreateCouponDto
    {
        public string Code { get; set; } = string.Empty;
        public string DiscountType { get; set; } = string.Empty; 
        public decimal DiscountValue { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool IsActive { get; set; } = true;
        public int? TotalUsageLimit { get; set; }
        public int? LimitPerUser { get; set; }
        public decimal? MinimumOrderValue { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public bool FirstTimeOnly { get; set; } = false;
        public string? ApplicableCategories { get; set; }
        public string? Description { get; set; }
    }
}
