using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Shared.DTOs.Coupons;
using PaperaX.Application.Interfaces;
using PaperaX.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Coupons.Queries.GetCoupons
{
    public class GetCouponsQueryHandler : IRequestHandler<GetCouponsQuery, IEnumerable<CouponDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCouponsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CouponDto>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
        {
            var coupons = await _context.Coupons
                .OrderByDescending(c => c.CreatedAt)
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
                }).ToListAsync(cancellationToken);

            return coupons;
        }
    }
}
