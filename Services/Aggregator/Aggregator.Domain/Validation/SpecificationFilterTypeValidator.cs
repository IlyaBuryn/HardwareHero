using Aggregator.Specifications.DTO;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.Domain.Validation
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
