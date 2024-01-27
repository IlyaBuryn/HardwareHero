using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
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
