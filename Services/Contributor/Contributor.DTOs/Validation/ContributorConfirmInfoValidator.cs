using Contributor.DTOs.Domain.Contributors;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.DTOs.Validation
{
    public class ContributorConfirmInfoValidator : AbstractValidator<ContributorConfirmInfoDto>
    {
        public ContributorConfirmInfoValidator()
        {
            RuleFor(c => c.IsConfirmed).NotEmpty()
                    .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.TimeStamp).NotEmpty()
                    .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
