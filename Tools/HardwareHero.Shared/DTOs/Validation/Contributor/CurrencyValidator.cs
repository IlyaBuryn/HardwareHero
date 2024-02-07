using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class CurrencyValidator : AbstractValidator<CurrencyDto>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Icon).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ImageData).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
