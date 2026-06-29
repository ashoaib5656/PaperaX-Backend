using System;

namespace PaperaX.Shared.DTOs.Customers
{
    public class UpdateCustomerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
