using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Configurator;

namespace HardwareHero.Shared.DTOs.Validation.Configurator
{
    public class StoredAssemblyValidator : AbstractValidator<StoredAssemblyDto>
    {
        public StoredAssemblyValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.SelectedComponents).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
