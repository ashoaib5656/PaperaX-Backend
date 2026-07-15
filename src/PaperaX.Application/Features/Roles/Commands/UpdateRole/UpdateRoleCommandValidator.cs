using FluentValidation;

namespace PaperaX.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Role ID is required.");
        }
    }
}
