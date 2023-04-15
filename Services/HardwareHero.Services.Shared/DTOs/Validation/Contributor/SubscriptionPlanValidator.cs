using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    public class SubscriptionPlanValidator : AbstractValidator<SubscriptionPlanDto>
    {
        public SubscriptionPlanValidator()
        {
            RuleFor(c => c.Price).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.DaysCount).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.PriorityLevel).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
