using System;

namespace PaperaX.Domain.Entities
{
    public class BannerVersion
    {
        public int Id { get; set; }
        public int BannerId { get; set; }
        public Banner Banner { get; set; } = null!;

        public int VersionNumber { get; set; }
        public string SnapshotJson { get; set; } = string.Empty;
        
        public string ChangedBy { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    }
}
