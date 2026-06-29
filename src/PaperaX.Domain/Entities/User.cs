using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? GoogleId { get; set; }
        public bool IsEmailVerified { get; set; }
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Invite Token Fields
        public string? InviteToken { get; set; }
        public DateTime? InviteTokenExpiry { get; set; }
        
        // Mock Data Properties
        public string Company { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int OrdersCount { get; set; }
        public decimal TotalSpent { get; set; }

    }
}
