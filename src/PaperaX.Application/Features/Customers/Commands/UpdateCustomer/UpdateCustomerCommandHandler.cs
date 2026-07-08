using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Shared.DTOs.Customers;
using PaperaX.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCustomerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.Customer == null)
            {
                throw new ArgumentNullException(nameof(request.Customer));
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id && u.Role == "Customer", cancellationToken);

            if (user == null)
            {
                throw new Exception($"Customer with ID {request.Id} not found.");
            }

            user.FullName = request.Customer.Name;
            user.Company = request.Customer.Company;
            user.Phone = request.Customer.Phone;
            user.Type = request.Customer.Type;
            user.Status = request.Customer.Status;

            await _context.SaveChangesAsync(cancellationToken);

            return new CustomerDto
            {
                UserId = user.Id,
                Id = $"#PX-C0{Math.Abs(user.Id)}",
                Name = user.FullName,
                Company = string.IsNullOrEmpty(user.Company) ? "N/A" : user.Company,
                Email = user.Email,
                Phone = user.Phone,
                Type = user.Type,
                Orders = user.OrdersCount,
                Spent = $"₹{user.TotalSpent:N0}",
                Status = user.Status
            };
        }
    }
}
