using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Prices;

namespace HardwareHero.Shared.DTOs.Validation.Prices
{
    public class MaintenanceReferencesValidator : AbstractValidator<MaintenanceReferencesDto>
    {
        public MaintenanceReferencesValidator()
        {
            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.MaintenanceId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Link).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
