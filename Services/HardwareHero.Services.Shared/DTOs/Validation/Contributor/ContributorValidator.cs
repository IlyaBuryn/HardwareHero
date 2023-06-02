using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    public class ContributorValidator : AbstractValidator<ContributorDto>
    {
        public ContributorValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Region).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Region).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.ContributorExcellence).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
