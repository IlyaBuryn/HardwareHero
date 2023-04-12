using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.DTOs.Validation.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    [Validator(typeof(ContributorDto))]
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

            RuleFor(c => c.SubscriptionInfoId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ContributorExcellenceId).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
