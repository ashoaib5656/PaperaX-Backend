using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
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
        public bool IsActive { get; set; } = true;
        public Dictionary<string, string> Specifications { get; set; } = new Dictionary<string, string>();
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Sku = request.Sku,
                Description = request.Description,
                LongDescription = request.LongDescription,
                Price = request.Price,
                Unit = request.Unit,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId,
                ImageUrl = request.ImageUrl,
                Status = request.Status,
                IsActive = true, // Force active when created
                Specifications = request.Specifications
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
