using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentTypeValidator : AbstractValidator<ComponentTypeDto>
    {
        public ComponentTypeValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.FullName).MaximumLength(ValidationValues.NameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.DescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);
        }
    }
}
