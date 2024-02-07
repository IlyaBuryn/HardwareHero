using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Configurator;

namespace HardwareHero.Shared.DTOs.Validation.Configurator
{
    public class ComponentTypeSignsValidator : AbstractValidator<ComponentTypeSignsDto>
    {
        public ComponentTypeSignsValidator()
        {
            RuleFor(c => c.ComponentNames).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Specifications).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
