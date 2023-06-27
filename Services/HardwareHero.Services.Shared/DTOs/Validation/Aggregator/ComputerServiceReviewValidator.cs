using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComputerServiceReviewValidator : AbstractValidator<ComputerServiceReviewDto>
    {
        public ComputerServiceReviewValidator()
        {
            RuleFor(c => c.Name).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.ComputerServiceId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
