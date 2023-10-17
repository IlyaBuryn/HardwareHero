using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class MaintenanceTypesValidator : AbstractValidator<MaintenanceTypeDto>
    {
        public MaintenanceTypesValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Name).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
