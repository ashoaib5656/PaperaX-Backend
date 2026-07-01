using MediatR;
namespace PaperaX.Application.Features.PromotionTypes.Commands.CreatePromotionType
{
    public class CreatePromotionTypeCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
