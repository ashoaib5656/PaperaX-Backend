using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Status { get; set; } = "In Stock";
        public bool? IsActive { get; set; }
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
            
            if (product == null) return false;

            product.Name = request.Name;
            product.Sku = request.Sku;
            product.Description = request.Description;
            product.LongDescription = request.LongDescription;
            product.Price = request.Price;
            product.Unit = request.Unit;
            product.StockQuantity = request.StockQuantity;
            product.CategoryId = request.CategoryId;
            product.ImageUrl = request.ImageUrl;
            product.Status = request.Status;
            product.Specifications = request.Specifications;
            
            // Allow explicit activation/deactivation via payload
            if (request.IsActive.HasValue) 
            {
                product.IsActive = request.IsActive.Value;
            }
            else if (request.StockQuantity > 0)
            {
                // Auto-activate if stock added and no explicit flag sent
                product.IsActive = true;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
