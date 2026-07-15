using System;

namespace PaperaX.Domain.Entities
{
    public class RoleAudit
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? PerformedBy { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Role Role { get; set; } = null!;
    }
}
