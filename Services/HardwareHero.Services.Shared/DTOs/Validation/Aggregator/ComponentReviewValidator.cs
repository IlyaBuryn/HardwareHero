using FluentValidation;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentReviewValidator : AbstractValidator<ComponentReviewDto>
    {
        public ComponentReviewValidator()
        {
            RuleFor(c => c.Name).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ContributorName).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
