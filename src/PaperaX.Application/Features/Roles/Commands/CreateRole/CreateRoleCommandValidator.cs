using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PaperaX.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PaperaX.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoleCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");

            RuleFor(v => v.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.")
                .MustAsync(BeUniqueCode).WithMessage("The specified code already exists.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .AllAsync(r => r.Name.ToLower() != name.ToLower(), cancellationToken);
        }

        public async Task<bool> BeUniqueCode(string code, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .AllAsync(r => r.Code.ToLower() != code.ToLower(), cancellationToken);
        }
    }
}
