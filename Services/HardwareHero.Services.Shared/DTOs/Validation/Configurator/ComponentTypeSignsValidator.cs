using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Configurator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Configurator
{
    public class ComponentTypeSignsValidator : AbstractValidator<ComponentTypeSignsDto>
    {
        public ComponentTypeSignsValidator()
        {
            RuleFor(c => c.ComponentNames).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Specifications).NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}
