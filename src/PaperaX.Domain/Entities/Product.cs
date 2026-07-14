using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperaX.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty;
        public string Status { get; set; } = "In Stock";
        public bool IsActive { get; set; } = true;
        public string Sku { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
    }
}
