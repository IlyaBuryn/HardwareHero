using Aggregator.DTOs.Specifications;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class SpecificationAttributeValidator : AbstractValidator<SpecificationAttributeDto>
    {
        public SpecificationAttributeValidator()
        {
            RuleFor(c => c.ComponentTypeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.SpecificationCategoryId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Key).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Key).MaximumLength(64)
                .WithMessage(ValidationMessages.MaximumLength);
        }
    }
}
