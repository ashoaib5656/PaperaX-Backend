using System;

namespace PaperaX.Domain.Entities
{
    public class BannerAnalytics
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; } = null!;
        
        public int Views { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }
        public decimal RevenueGenerated { get; set; }
        
        public DateTime Date { get; set; } // Tracked daily
    }
}
