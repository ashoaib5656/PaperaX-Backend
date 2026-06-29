using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperaX.Shared.DTOs.Promotions;
using PaperaX.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Promotions.Queries.GetPromotionById
{
    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, PromotionDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetPromotionByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PromotionDto?> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var p = await _context.Promotions.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            
            if (p == null) return null;

            return new PromotionDto
            {
                Id = p.Id,
                CampaignName = p.CampaignName,
                PromotionType = p.PromotionType,
                DiscountValue = p.DiscountValue,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                Status = p.Status,
                AppliesTo = p.AppliesTo,
                ApplicableCategories = p.ApplicableCategories,
                ApplicableProducts = p.ApplicableProducts,
                MinimumOrderValue = p.MinimumOrderValue,
                MaximumDiscountAmount = p.MaximumDiscountAmount,
                Priority = p.Priority,
                BannerImage = p.BannerImage,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            };
        }
    }
}

