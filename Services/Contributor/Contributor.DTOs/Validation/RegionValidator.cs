using Contributor.DTOs.Domain.Regions;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
{
    public class RegionValidator : AbstractValidator<RegionDto>
    {
        public RegionValidator()
        {
            RuleFor(c => c.Country).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Code).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
