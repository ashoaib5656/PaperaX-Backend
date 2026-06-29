using MediatR;
using PaperaX.Shared.DTOs.Coupons;

namespace PaperaX.Application.Features.Coupons.Commands.UpdateCoupon
{
    public class UpdateCouponCommand : IRequest<bool>
    {
        public UpdateCouponDto CouponDto { get; set; } = new UpdateCouponDto();
    }
}
