using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperaX.Shared.DTOs.Promotions;
using PaperaX.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Promotions.Queries.GetAllPromotions
{
    public class GetAllPromotionsQueryHandler : IRequestHandler<GetAllPromotionsQuery, IEnumerable<PromotionDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllPromotionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PromotionDto>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
        {
            var promotions = await _context.Promotions.AsNoTracking().ToListAsync(cancellationToken);
            
            return promotions.Select(p => new PromotionDto
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
            });
        }
    }
}

