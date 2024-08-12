using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Prices;

namespace HardwareHero.Shared.DTOs.Validation.Prices
{
    public class ContributorPricesValidator : AbstractValidator<ContributorComponentPricesDto>
    {
        public ContributorPricesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Prices).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
