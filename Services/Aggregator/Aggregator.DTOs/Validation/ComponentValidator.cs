using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentValidator : AbstractValidator<ComponentDto>
    {
        public ComponentValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.ComponentNameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.DescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.ComponentTypeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
