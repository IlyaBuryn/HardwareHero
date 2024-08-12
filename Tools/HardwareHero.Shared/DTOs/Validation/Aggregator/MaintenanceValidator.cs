using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Aggregator;

namespace HardwareHero.Shared.DTOs.Validation.Aggregator
{
    public class MaintenanceValidator : AbstractValidator<MaintenanceDto>
    {
        public MaintenanceValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.DescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.MaintenanceTypeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
