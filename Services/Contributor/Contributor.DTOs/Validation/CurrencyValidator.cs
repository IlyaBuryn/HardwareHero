using Contributor.DTOs.Domain.Currencies;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
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
