using MediatR;
using PaperaX.Domain.Entities;

namespace PaperaX.Application.Features.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQuery : IRequest<Brand?>
    {
        public int Id { get; set; }

        public GetBrandByIdQuery(int id)
        {
            Id = id;
        }
    }
}
