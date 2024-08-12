using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class SubscriptionPlanValidator : AbstractValidator<SubscriptionPlanDto>
    {
        public SubscriptionPlanValidator()
        {
            RuleFor(c => c.Price).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.DaysCount).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.PriorityLevel).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
