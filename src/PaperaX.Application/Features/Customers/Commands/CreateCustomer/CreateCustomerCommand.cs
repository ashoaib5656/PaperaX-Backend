using MediatR;
using PaperaX.Shared.DTOs.Customers;

namespace PaperaX.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public CreateCustomerDto? Customer { get; set; }
    }
}
