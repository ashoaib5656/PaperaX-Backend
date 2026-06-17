using MediatR;

namespace PaperaX.Application.Features.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBrandCommand(int id)
        {
            Id = id;
        }
    }
}
