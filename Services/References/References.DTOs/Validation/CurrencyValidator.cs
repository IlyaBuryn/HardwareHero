using FluentValidation;
using HardwareHero.Shared.Constants;
using References.DTOs.Domain;

namespace References.DTOs.Validation
{
    public class CurrencyValidator : AbstractValidator<CurrencyDto>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.Code).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
