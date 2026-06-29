using MediatR;
using PaperaX.Shared.DTOs.Coupons;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Coupons.Queries.GetCoupons
{
    public class GetCouponsQuery : IRequest<IEnumerable<CouponDto>>
    {
    }
}
