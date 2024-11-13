using Aggregator.DTOs.Specifications;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class SpecificationCategoryValidator : AbstractValidator<SpecificationCategoryDto>
    {
        public SpecificationCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
