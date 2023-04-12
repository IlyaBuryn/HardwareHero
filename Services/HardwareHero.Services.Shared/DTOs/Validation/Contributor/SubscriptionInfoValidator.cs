using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.DTOs.Validation.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    [Validator(typeof(SubscriptionInfoDto))]
    public class SubscriptionInfoValidator : AbstractValidator<SubscriptionInfoDto>
    {
        public SubscriptionInfoValidator()
        {
            RuleFor(c => c.PlanId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
