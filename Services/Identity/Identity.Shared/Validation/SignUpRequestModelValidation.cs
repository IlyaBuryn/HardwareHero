using FluentValidation;
using HardwareHero.Shared.Constants;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Shared.Validation
{
    public class SignUpRequestModelValidation : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestModelValidation()
        {
            RuleFor(s => s.Email).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(s => s.Username).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(s => s.Password).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(s => s.StayIn).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(s => s.CallbackUrl).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
