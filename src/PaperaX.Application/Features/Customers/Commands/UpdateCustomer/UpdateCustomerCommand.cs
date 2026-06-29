using MediatR;
using PaperaX.Shared.DTOs.Customers;

namespace PaperaX.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public int Id { get; set; }
        public UpdateCustomerDto? Customer { get; set; }
    }
}
