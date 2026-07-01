using MediatR;
using System.Collections.Generic;
using PaperaX.Shared.DTOs.PromotionTypes;
namespace PaperaX.Application.Features.PromotionTypes.Queries.GetAllPromotionTypes
{
    public class GetAllPromotionTypesQuery : IRequest<IEnumerable<PromotionTypeDto>> { }
}
