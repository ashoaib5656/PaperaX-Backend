using MediatR;
namespace PaperaX.Application.Features.PromotionTypes.Commands.UpdatePromotionType
{
    public class UpdatePromotionTypeCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
