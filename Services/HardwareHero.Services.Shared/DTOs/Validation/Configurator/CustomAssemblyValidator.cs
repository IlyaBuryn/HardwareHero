using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Configurator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Configurator
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
