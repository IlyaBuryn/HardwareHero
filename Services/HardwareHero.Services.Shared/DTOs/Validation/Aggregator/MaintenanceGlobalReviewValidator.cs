using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class MaintenanceGlobalReviewValidator : AbstractValidator<MaintenanceGlobalReviewDto>
    {
        public MaintenanceGlobalReviewValidator()
        {
            RuleFor(c => c.AuthorName).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.MaintenanceId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Rating).GreaterThan(0)
                .WithMessage("{PropertyName} should be greater than {ComparisonValue}.");

            RuleFor(c => c.Rating).LessThan(6)
                .WithMessage("{PropertyName} should be less than {ComparisonValue}.");
        }
    }
}
