using Contributor.Domain.DTO.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ContributorUserAccountValidator 
        : AbstractValidator<ContributorUserAccountDto>
    {
        public ContributorUserAccountValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                    .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
