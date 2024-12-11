using Aggregator.Domain.Components.DTO;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.Domain.Validation
{
    public class ComponentLocalReviewValidator 
        : AbstractValidator<ComponentLocalReviewDto>
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
