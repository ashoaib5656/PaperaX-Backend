using MediatR;
using PaperaX.Shared.DTOs.Promotions;

namespace PaperaX.Application.Features.Promotions.Queries.GetPromotionById
{
    public class GetPromotionByIdQuery : IRequest<PromotionDto?>
    {
        public int Id { get; set; }
    }
}
