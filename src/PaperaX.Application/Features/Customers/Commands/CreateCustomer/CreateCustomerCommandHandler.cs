using MediatR;
using PaperaX.Shared.DTOs.Customers;
using PaperaX.Application.Interfaces;
using PaperaX.Application.Interfaces;
using PaperaX.Application.Features.Auth.Interfaces;
using PaperaX.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public CreateCustomerCommandHandler(IApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (request.Customer == null)
            {
                throw new ArgumentNullException(nameof(request.Customer));
            }

            var user = new User
            {
                FullName = request.Customer.Name,
                Company = request.Customer.Company,
                Email = request.Customer.Email,
                Phone = request.Customer.Phone,
                Type = request.Customer.Type,
                Status = request.Customer.SendInvite ? "Pending Setup" : request.Customer.Status,
                Role = "Customer",
                CreatedAt = DateTime.UtcNow,
                OrdersCount = 0,
                TotalSpent = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Customer.SendInvite)
            {
                var inviteToken = Guid.NewGuid().ToString("N");
                user.InviteToken = inviteToken;
                user.InviteTokenExpiry = DateTime.UtcNow.AddHours(48);
                await _context.SaveChangesAsync(cancellationToken);
                
                await _emailService.SendCustomerInviteAsync(user.Email, user.FullName, inviteToken);
            }
            else
            {
                var defaultPassword = Guid.NewGuid().ToString("N").Substring(0, 8) + "X@";
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);
                user.Status = "Active"; // Since password is set
                await _context.SaveChangesAsync(cancellationToken);
                
                await _emailService.SendCustomerWelcomeWithPasswordAsync(user.Email, user.FullName, defaultPassword);
            }

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
