using MediatR;
using PaperaX.Domain.Entities;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
