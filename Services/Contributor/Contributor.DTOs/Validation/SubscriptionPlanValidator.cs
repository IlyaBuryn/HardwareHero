using Contributor.DTOs.Domain.Subscription;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
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
