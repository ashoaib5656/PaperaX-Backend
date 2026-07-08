using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly IApplicationDbContext _context;

        public GetCategoryByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        }
    }
}
