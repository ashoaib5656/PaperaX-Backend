using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string DeliveryMethod { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Mock data properties
        public string StringId { get; set; } = string.Empty; // e.g. #PX-ORDER-4921
        public string DateString { get; set; } = string.Empty;
        public string ItemsJson { get; set; } = string.Empty;
        public string ItemSummary { get; set; } = string.Empty;
        public string TotalString { get; set; } = string.Empty;
        public string EstDelivery { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Payment { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }
}
