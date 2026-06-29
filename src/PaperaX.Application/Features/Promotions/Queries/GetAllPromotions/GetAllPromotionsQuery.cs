using System.Collections.Generic;
using MediatR;
using PaperaX.Shared.DTOs.Promotions;

namespace PaperaX.Application.Features.Promotions.Queries.GetAllPromotions
{
    public class GetAllPromotionsQuery : IRequest<IEnumerable<PromotionDto>>
    {
    }
}
