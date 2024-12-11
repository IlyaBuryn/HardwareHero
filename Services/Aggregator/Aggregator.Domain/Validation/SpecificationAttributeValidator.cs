using Aggregator.Specifications.DTO;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.Domain.Validation
{
    public class SpecificationAttributeValidator 
        : AbstractValidator<SpecificationAttributeDto>
    {
        public SpecificationAttributeValidator()
        {
            RuleFor(c => c.ComponentTypeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.SpecificationCategoryId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.AttributeKey).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.AttributeKey).MaximumLength(64)
                .WithMessage(ValidationMessages.MaximumLength);
        }
    }
}
