using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Aggregator;

namespace HardwareHero.Shared.DTOs.Validation.Aggregator
{
    public class ComponentAttributesValidator : AbstractValidator<ComponentAttributesDto>
    {
        public ComponentAttributesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.AttributeName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.AttributeValue).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
