using Contributor.Domain.DTO.Subscription;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class SubscriptionPlanValidator
        : AbstractValidator<SubscriptionPlanDto>
    {
        public SubscriptionPlanValidator()
        {
            RuleFor(c => c.CurrencyId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.DaysCount).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
