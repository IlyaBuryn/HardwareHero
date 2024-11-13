using FluentValidation;
using HardwareHero.Shared.Constants;
using Prices.DTOs.Prices;

namespace Prices.DTOs.Validation
{
    public class ContributorPricesValidator  : AbstractValidator<ContributorComponentPricesDto>
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
