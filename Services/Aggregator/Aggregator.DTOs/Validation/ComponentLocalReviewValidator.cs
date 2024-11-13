using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentLocalReviewValidator : AbstractValidator<ComponentLocalReviewDto>
    {
        public ComponentLocalReviewValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.IsRecommended).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
