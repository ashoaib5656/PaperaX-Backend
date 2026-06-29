using MediatR;
using PaperaX.Shared.DTOs.Customers;
using System.Collections.Generic;

namespace PaperaX.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<CustomerDto>>
    {
    }
}
