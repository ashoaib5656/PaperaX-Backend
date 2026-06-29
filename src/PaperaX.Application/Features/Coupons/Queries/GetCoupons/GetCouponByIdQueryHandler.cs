using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Shared.DTOs.Coupons;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Coupons.Queries.GetCoupons
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponDto?>
    {
        private readonly IApplicationDbContext _context;

        public GetCouponByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CouponDto?> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _context.Coupons
                .Where(c => c.Id == request.Id)
                .Select(c => new CouponDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    DiscountType = c.DiscountType,
                    DiscountValue = c.DiscountValue,
                    ValidFrom = c.ValidFrom,
                    ValidUntil = c.ValidUntil,
                    IsActive = c.IsActive,
                    TotalUsageLimit = c.TotalUsageLimit,
                    CurrentUsageCount = c.CurrentUsageCount,
                    LimitPerUser = c.LimitPerUser,
                    MinimumOrderValue = c.MinimumOrderValue,
                    MaximumDiscountAmount = c.MaximumDiscountAmount,
                    FirstTimeOnly = c.FirstTimeOnly,
                    ApplicableCategories = c.ApplicableCategories,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                }).FirstOrDefaultAsync(cancellationToken);

            return coupon;
        }
    }
}
