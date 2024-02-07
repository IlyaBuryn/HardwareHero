using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
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
