using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Aggregator;

namespace HardwareHero.Shared.DTOs.Validation.Aggregator
{
    public class MaintenanceTypesValidator : AbstractValidator<MaintenanceTypeDto>
    {
        public MaintenanceTypesValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);
        }
    }
}
