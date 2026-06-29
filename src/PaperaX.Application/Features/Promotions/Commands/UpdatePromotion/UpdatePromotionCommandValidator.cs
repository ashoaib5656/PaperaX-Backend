using FluentValidation;

namespace PaperaX.Application.Features.Promotions.Commands.UpdatePromotion
{
    public class UpdatePromotionCommandValidator : AbstractValidator<UpdatePromotionCommand>
    {
        public UpdatePromotionCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id is required and must be greater than zero.");

            RuleFor(v => v.CampaignName)
                .NotEmpty().WithMessage("Campaign Name is required.")
                .MaximumLength(200).WithMessage("Campaign Name must not exceed 200 characters.");

            RuleFor(v => v.PromotionType)
                .NotEmpty().WithMessage("Promotion Type is required.");

            RuleFor(v => v.DiscountValue)
                .GreaterThan(0).WithMessage("Discount Value must be greater than zero.");

            RuleFor(v => v.StartTime)
                .NotEmpty().WithMessage("Start Time is required.");

            RuleFor(v => v.EndTime)
                .NotEmpty().WithMessage("End Time is required.")
                .GreaterThan(v => v.StartTime).WithMessage("End Time must be after Start Time.");

            RuleFor(v => v.Status)
                .NotEmpty().WithMessage("Status is required.");
                
            RuleFor(v => v.AppliesTo)
                .NotEmpty().WithMessage("AppliesTo is required.");
        }
    }
}
