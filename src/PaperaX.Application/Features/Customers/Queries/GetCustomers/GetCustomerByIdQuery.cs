using MediatR;
using PaperaX.Shared.DTOs.Customers;

namespace PaperaX.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; }
    }
}
