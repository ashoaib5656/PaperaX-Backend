using MediatR;
using PaperaX.Shared.DTOs.Coupons;

namespace PaperaX.Application.Features.Coupons.Queries.GetCoupons
{
    public class GetCouponByIdQuery : IRequest<CouponDto?>
    {
        public int Id { get; set; }
        public GetCouponByIdQuery(int id) { Id = id; }
    }
}
