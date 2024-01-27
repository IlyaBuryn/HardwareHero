using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
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
