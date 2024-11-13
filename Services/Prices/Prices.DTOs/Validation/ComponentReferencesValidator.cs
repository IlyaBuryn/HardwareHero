using FluentValidation;
using HardwareHero.Shared.Constants;
using Prices.DTOs.Prices;

namespace Prices.DTOs.Validation
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
