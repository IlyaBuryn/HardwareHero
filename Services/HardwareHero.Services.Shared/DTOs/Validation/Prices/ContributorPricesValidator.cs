using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Prices;

namespace HardwareHero.Services.Shared.DTOs.Validation.Prices
{
    public class ContributorPricesValidator : AbstractValidator<ContributorPriceDto>
    {
        public ContributorPricesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Pricestamp).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Pricestamp).GreaterThan(0)
                .WithMessage("{PropertyName} should be more than {ComparisonValue}!");
        }
    }
}
