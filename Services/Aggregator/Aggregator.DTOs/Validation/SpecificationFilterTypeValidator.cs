using Aggregator.DTOs.Specifications;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class SpecificationFilterTypeValidator 
        : AbstractValidator<SpecificationFilterTypeDto>
    {
        public SpecificationFilterTypeValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
