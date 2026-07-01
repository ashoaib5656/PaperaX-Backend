using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperaX.Domain.Entities;
using PaperaX.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Promotions.Commands.CreatePromotion
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePromotionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = new Promotion
            {
                CampaignName = request.CampaignName,
                PromotionType = request.PromotionType,
                DiscountValue = request.DiscountValue,
                DiscountText = request.DiscountText,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = request.Status,
                AppliesTo = request.AppliesTo,
                ApplicableCategories = request.ApplicableCategories,
                ApplicableProducts = request.ApplicableProducts,
                MinimumOrderValue = request.MinimumOrderValue,
                MaximumDiscountAmount = request.MaximumDiscountAmount,
                Priority = request.Priority,
                BannerImage = request.BannerImage,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync(cancellationToken);
            return promotion.Id;
        }
    }
}
