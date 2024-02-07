using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Prices;

namespace HardwareHero.Shared.DTOs.Validation.Prices
{
    public class ContributorPricesValidator : AbstractValidator<ContributorPriceDto>
    {
        public ContributorPricesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Pricestamp).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Pricestamp).GreaterThan(ValidationValues.PriceFromNotIncluding)
                .WithMessage(ValidationMessages.GreaterThan);
        }
    }
}
