using Contributor.DTOs.Domain.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
{
    public class ContributorExcellenceValidator : AbstractValidator<ContributorExcellenceDto>
    {
        public ContributorExcellenceValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            //RuleFor(c => c.LogoUrl).NotEmpty()
            //    .WithMessage(ValidationMessages.IsRequired);

            //RuleFor(c => c.ImageData).NotEmpty()
            //    .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.ContributorDescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Phone).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Phone).MaximumLength(ValidationValues.PhoneMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.RegionId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.CurrencyId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
