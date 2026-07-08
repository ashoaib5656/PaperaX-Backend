using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Shared.DTOs.Customers;
using PaperaX.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace PaperaX.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomerByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Users
                .Where(u => u.Role == "Customer" && u.Id == request.Id)
                .Select(u => new CustomerDto
                {
                    UserId = u.Id,
                    Id = $"#PX-C0{Math.Abs(u.Id)}",
                    Name = u.FullName,
                    Company = string.IsNullOrEmpty(u.Company) ? "N/A" : u.Company,
                    Email = u.Email,
                    Phone = u.Phone,
                    Type = u.Type,
                    Orders = u.OrdersCount,
                    Spent = $"₹{u.TotalSpent:N0}",
                    Status = u.Status
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
            {
                throw new Exception($"Customer with ID {request.Id} not found.");
            }

            return customer;
        }
    }
}
