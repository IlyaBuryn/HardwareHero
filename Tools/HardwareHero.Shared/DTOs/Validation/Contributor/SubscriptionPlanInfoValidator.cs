using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class SubscriptionPlanInfoValidator : AbstractValidator<SubscriptionPlanInfoDto>
    {
        public SubscriptionPlanInfoValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
