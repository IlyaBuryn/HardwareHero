using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Configurator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Configurator
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
