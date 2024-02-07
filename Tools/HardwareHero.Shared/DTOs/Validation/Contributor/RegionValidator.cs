using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class RegionValidator : AbstractValidator<RegionDto>
    {
        public RegionValidator()
        {
            RuleFor(c => c.Country).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
