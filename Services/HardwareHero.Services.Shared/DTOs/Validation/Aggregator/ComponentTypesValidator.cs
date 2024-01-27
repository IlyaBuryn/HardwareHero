using FluentValidation;
using HardwareHero.Services.Shared.Constants;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentTypesValidator : AbstractValidator<ComponentTypeDto>
    {
        public ComponentTypesValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.FullName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.FullName).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.DescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);
        }
    }
}
