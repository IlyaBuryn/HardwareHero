using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentAttributeValidator : AbstractValidator<ComponentAttributeDto>
    {
        public ComponentAttributeValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.SpecificationAttributeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
