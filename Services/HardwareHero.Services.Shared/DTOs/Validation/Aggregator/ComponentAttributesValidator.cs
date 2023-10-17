using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentAttributesValidator : AbstractValidator<ComponentAttributesDto>
    {
        public ComponentAttributesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.AttributeName).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.AttributeValue).NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}
