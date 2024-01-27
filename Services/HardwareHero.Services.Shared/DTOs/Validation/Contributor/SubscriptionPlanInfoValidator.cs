using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
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
