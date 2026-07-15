using System;

namespace PaperaX.Domain.Entities
{
    public class MenuAudit
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Action { get; set; } = string.Empty; // Create, Update, Delete
        public string? OldValue { get; set; } // JSON snapshot
        public string? NewValue { get; set; } // JSON snapshot
        public int? PerformedBy { get; set; }
        public User? PerformedByUser { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
