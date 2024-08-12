using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Prices;

namespace HardwareHero.Shared.DTOs.Validation.Prices
{
    public class ComponentReferencesValidator : AbstractValidator<ComponentReferencesDto>
    {
        public ComponentReferencesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Link).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
