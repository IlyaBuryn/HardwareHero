using Aggregator.Domain.Components.DTO;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.Domain.Validation
{
    public class ComponentGlobalReviewValidator 
        : AbstractValidator<ComponentGlobalReviewDto>
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
