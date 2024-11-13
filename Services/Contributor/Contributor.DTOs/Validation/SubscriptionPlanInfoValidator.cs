using Contributor.DTOs.Domain.Subscription;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
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
