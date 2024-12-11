using Contributor.Domain.DTO.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ContributorModelValidator 
        : AbstractValidator<ContributorModelDto>
    {
        public ContributorModelValidator()
        {
            RuleFor(c => c.ContributorUserAccount).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorDetails).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
