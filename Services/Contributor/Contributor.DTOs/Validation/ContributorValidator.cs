using Contributor.DTOs.Domain.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
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
