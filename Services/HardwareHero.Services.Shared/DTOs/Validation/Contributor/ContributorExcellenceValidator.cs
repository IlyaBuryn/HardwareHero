using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
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
