using Contributor.Domain.DTO.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ContributorDetailsValidator
        : AbstractValidator<ContributorDetailsDto>
    {
        public ContributorDetailsValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.CompanyName).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.ContributorDescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.CompanyPhone).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.CompanyPhone).MaximumLength(ValidationValues.PhoneMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.CompanyRegionId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.CompanyCurrencyId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
