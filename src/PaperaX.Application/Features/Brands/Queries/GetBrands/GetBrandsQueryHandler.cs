using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using PaperaX.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Brands.Queries.GetBrands
{
    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, List<Brand>>
    {
        private readonly IApplicationDbContext _context;

        public GetBrandsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Brands.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
