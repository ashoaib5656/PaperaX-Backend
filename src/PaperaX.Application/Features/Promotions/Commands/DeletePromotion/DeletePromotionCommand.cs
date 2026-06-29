using MediatR;

namespace PaperaX.Application.Features.Promotions.Commands.DeletePromotion
{
    public class DeletePromotionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
