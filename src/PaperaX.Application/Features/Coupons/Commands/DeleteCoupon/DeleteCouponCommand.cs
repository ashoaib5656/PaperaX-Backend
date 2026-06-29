using MediatR;

namespace PaperaX.Application.Features.Coupons.Commands.DeleteCoupon
{
    public class DeleteCouponCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteCouponCommand(int id) { Id = id; }
    }
}
