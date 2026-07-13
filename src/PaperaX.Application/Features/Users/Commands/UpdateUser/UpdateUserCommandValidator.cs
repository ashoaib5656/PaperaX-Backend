using FluentValidation;

namespace PaperaX.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.Request.FullName)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Request.Phone)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(v => v.Request.Address)
                .NotEmpty();
        }
    }
}
