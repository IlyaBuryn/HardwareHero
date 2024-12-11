using Contributor.Domain.DTO.Subscription;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ContributorSubscriptionPlanValidator
        : AbstractValidator<ContributorSubscriptionPlanDto>
    {
        public ContributorSubscriptionPlanValidator()
        {
            RuleFor(c => c.SubscriptionPlanId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
