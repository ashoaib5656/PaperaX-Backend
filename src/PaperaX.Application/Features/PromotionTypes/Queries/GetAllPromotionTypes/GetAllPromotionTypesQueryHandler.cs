using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Shared.DTOs.PromotionTypes;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.PromotionTypes.Queries.GetAllPromotionTypes
{
    public class GetAllPromotionTypesQueryHandler : IRequestHandler<GetAllPromotionTypesQuery, IEnumerable<PromotionTypeDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllPromotionTypesQueryHandler(IApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<PromotionTypeDto>> Handle(GetAllPromotionTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.PromotionTypes
                .Select(p => new PromotionTypeDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    IsActive = p.IsActive,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
