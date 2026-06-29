using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PaperaX.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PaperaX.Application.Features.Promotions.Commands.UpdatePromotion
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePromotionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _context.Promotions.FindAsync(new object[] { request.Id }, cancellationToken);

            if (promotion == null)
            {
                throw new Exception($"Promotion with id {request.Id} not found.");
            }

            promotion.CampaignName = request.CampaignName;
            promotion.PromotionType = request.PromotionType;
            promotion.DiscountValue = request.DiscountValue;
            promotion.StartTime = request.StartTime;
            promotion.EndTime = request.EndTime;
            promotion.Status = request.Status;
            promotion.AppliesTo = request.AppliesTo;
            promotion.ApplicableCategories = request.ApplicableCategories;
            promotion.ApplicableProducts = request.ApplicableProducts;
            promotion.MinimumOrderValue = request.MinimumOrderValue;
            promotion.MaximumDiscountAmount = request.MaximumDiscountAmount;
            promotion.Priority = request.Priority;
            promotion.BannerImage = request.BannerImage;
            promotion.Description = request.Description;
            promotion.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}

