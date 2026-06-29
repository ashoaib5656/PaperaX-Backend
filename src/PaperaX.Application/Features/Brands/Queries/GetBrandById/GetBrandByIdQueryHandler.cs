using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Brand?>
    {
        private readonly IApplicationDbContext _context;

        public GetBrandByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand?> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Brands.FindAsync(new object[] { request.Id }, cancellationToken);
        }
    }
}
