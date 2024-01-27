using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
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
