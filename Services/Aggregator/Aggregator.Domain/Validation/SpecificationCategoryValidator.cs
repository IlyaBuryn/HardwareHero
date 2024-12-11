using Aggregator.Specifications.DTO;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.Domain.Validation
{
    public class SpecificationCategoryValidator 
        : AbstractValidator<SpecificationCategoryDto>
    {
        public SpecificationCategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
