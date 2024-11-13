using Configurator.DTOs.Domain;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Configurator.DTOs.Validation
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
