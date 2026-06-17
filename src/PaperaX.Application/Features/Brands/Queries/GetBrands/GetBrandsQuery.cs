using MediatR;
using PaperaX.Domain.Entities;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Brands.Queries.GetBrands
{
    public class GetBrandsQuery : IRequest<List<Brand>>
    {
    }
}
