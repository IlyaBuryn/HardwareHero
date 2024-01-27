using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    public class ContributorExcellenceValidator : AbstractValidator<ContributorExcellenceDto>
    {
        public ContributorExcellenceValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Logo).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ImageData).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.ContributorDescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Phone).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Phone).MaximumLength(ValidationValues.PhoneMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Region).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
