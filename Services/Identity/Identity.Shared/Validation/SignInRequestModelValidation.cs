using FluentValidation;
using HardwareHero.Shared.Constants;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Shared.Validation
{
    public class SignInRequestModelValidation : AbstractValidator<SignInRequest>
    {
        public SignInRequestModelValidation()
        {
            RuleFor(s => s.UsernameOrEmail).NotEmpty()
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
