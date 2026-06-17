using MediatR;
using PaperaX.Domain.Entities;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }
}
