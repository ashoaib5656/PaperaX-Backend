using MediatR;
using PaperaX.Application.Interfaces;
using PaperaX.Shared.DTOs.PromotionTypes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.PromotionTypes.Queries.GetPromotionTypeById
{
    public class GetPromotionTypeByIdQueryHandler : IRequestHandler<GetPromotionTypeByIdQuery, PromotionTypeDto>
    {
        private readonly IApplicationDbContext _context;
        public GetPromotionTypeByIdQueryHandler(IApplicationDbContext context) { _context = context; }

        public async Task<PromotionTypeDto> Handle(GetPromotionTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.PromotionTypes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null) return null;

            return new PromotionTypeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
