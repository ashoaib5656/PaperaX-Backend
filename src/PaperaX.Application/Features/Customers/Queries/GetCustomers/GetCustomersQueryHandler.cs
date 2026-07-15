using MediatR;
using Microsoft.EntityFrameworkCore;
using PaperaX.Shared.DTOs.Customers;
using PaperaX.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace PaperaX.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Where(u => u.LegacyRole == "Customer")
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
                .ToListAsync(cancellationToken);
        }
    }
}
