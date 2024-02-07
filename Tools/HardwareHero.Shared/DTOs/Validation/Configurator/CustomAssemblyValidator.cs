using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Configurator;

namespace HardwareHero.Shared.DTOs.Validation.Configurator
{
    public class CustomAssemblyValidator : AbstractValidator<CustomAssemblyDto>
    {
        public CustomAssemblyValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ComponentIds).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
