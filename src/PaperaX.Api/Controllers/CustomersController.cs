using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaperaX.Shared.DTOs.Customers;
using PaperaX.Application.Features.Customers.Commands.CreateCustomer;
using PaperaX.Application.Features.Customers.Commands.DeleteCustomer;
using PaperaX.Application.Features.Customers.Commands.UpdateCustomer;
using PaperaX.Application.Features.Customers.Queries.GetCustomers;
using PaperaX.Application.Common.Models;
using System.Threading.Tasks;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            return Ok(ApiResponse<object>.Success(customers, "Customers retrieved successfully"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            if (customer == null)
            {
                return NotFound(ApiResponse<object>.Failure($"Customer with ID {id} not found"));
            }
            return Ok(ApiResponse<object>.Success(customer, "Customer retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var command = new CreateCustomerCommand { Customer = dto };
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "Customer created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto dto)
        {
            var command = new UpdateCustomerCommand { Id = id, Customer = dto };
            var result = await _mediator.Send(command);
            return Ok(ApiResponse<object>.Success(result, "Customer updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command);
            return Ok(ApiResponse<bool>.Success(true, "Customer deleted successfully"));
        }
    }
}
