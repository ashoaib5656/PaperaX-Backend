using MediatR;
namespace PaperaX.Application.Features.PromotionTypes.Commands.DeletePromotionType
{
    public class DeletePromotionTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
