using MediatR;

namespace PaperaX.Application.Features.Menus.Commands.DeleteMenu
{
    public class DeleteMenuCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int? PerformedByUserId { get; set; }
    }
}
