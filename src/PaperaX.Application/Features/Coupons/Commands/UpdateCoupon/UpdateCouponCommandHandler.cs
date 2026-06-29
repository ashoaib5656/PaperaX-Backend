using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Coupons.Commands.UpdateCoupon
{
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCouponCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CouponDto;

            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == dto.Id, cancellationToken);
            if (coupon == null) return false;

            if (coupon.Code != dto.Code && await _context.Coupons.AnyAsync(c => c.Code == dto.Code, cancellationToken))
            {
                throw new ValidationException($"A coupon with the code '{dto.Code}' already exists.");
            }

            coupon.Code = dto.Code;
            coupon.DiscountType = dto.DiscountType;
            coupon.DiscountValue = dto.DiscountValue;
            coupon.ValidFrom = dto.ValidFrom;
            coupon.ValidUntil = dto.ValidUntil;
            coupon.IsActive = dto.IsActive;
            coupon.TotalUsageLimit = dto.TotalUsageLimit;
            coupon.LimitPerUser = dto.LimitPerUser;
            coupon.MinimumOrderValue = dto.MinimumOrderValue;
            coupon.MaximumDiscountAmount = dto.MaximumDiscountAmount;
            coupon.FirstTimeOnly = dto.FirstTimeOnly;
            coupon.ApplicableCategories = dto.ApplicableCategories;
            coupon.Description = dto.Description;
            coupon.UpdatedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
