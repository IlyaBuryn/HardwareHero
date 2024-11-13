using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentGlobalReviewValidator : AbstractValidator<ComponentGlobalReviewDto>
    {
        public ComponentGlobalReviewValidator()
        {
            RuleFor(c => c.AuthorName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.IsRecommended).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
