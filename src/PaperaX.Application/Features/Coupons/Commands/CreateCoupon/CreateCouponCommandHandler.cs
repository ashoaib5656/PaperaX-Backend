using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCouponCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CouponDto;

            if (await _context.Coupons.AnyAsync(c => c.Code == dto.Code, cancellationToken))
            {
                throw new ValidationException($"A coupon with the code '{dto.Code}' already exists.");
            }

            var coupon = new Coupon
            {
                Code = dto.Code,
                DiscountType = dto.DiscountType,
                DiscountValue = dto.DiscountValue,
                ValidFrom = dto.ValidFrom,
                ValidUntil = dto.ValidUntil,
                IsActive = dto.IsActive,
                TotalUsageLimit = dto.TotalUsageLimit,
                LimitPerUser = dto.LimitPerUser,
                MinimumOrderValue = dto.MinimumOrderValue,
                MaximumDiscountAmount = dto.MaximumDiscountAmount,
                FirstTimeOnly = dto.FirstTimeOnly,
                ApplicableCategories = dto.ApplicableCategories,
                Description = dto.Description,
                CreatedAt = System.DateTime.UtcNow,
                CurrentUsageCount = 0
            };

            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync(cancellationToken);

            return coupon.Id;
        }
    }
}
