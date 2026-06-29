using MediatR;
using PaperaX.Domain.Entities;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                Slug = request.Slug,
                ImageUrl = request.ImageUrl,
                Code = request.Code,
                DisplayOrder = request.DisplayOrder,
                IsActive = request.IsActive,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
