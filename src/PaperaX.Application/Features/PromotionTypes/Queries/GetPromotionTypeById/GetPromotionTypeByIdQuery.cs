using MediatR;
using PaperaX.Shared.DTOs.PromotionTypes;
namespace PaperaX.Application.Features.PromotionTypes.Queries.GetPromotionTypeById
{
    public class GetPromotionTypeByIdQuery : IRequest<PromotionTypeDto>
    {
        public int Id { get; set; }
    }
}
