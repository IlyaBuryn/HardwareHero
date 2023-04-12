using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.DTOs.Validation.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    [Validator(typeof(ContributorExcellenceDto))]
    public class ContributorExcellenceValidator : AbstractValidator<ContributorExcellenceDto>
    {
        public ContributorExcellenceValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Name).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.Logo).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Logo).MaximumLength(512)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
