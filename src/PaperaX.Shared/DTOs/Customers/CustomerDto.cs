using System;

namespace PaperaX.Shared.DTOs.Customers
{
    public class CustomerDto
    {
        public string Id { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Orders { get; set; }
        public string Spent { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
