using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class ContributorValidator : AbstractValidator<ContributorModelDto>
    {
        public ContributorValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorExcellence).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
