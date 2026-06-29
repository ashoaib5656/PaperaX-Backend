using MediatR;
using PaperaX.Shared.DTOs.Coupons;

namespace PaperaX.Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommand : IRequest<int>
    {
        public CreateCouponDto CouponDto { get; set; } = new CreateCouponDto();
    }
}
