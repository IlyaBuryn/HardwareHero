using FluentValidation;
using HardwareHero.Shared.Constants;
using Identity.Shared.Requests;

namespace Identity.Shared.Validation
{
    public class TokenRequestModelsValidation : AbstractValidator<TokenRequest>
    {
        public TokenRequestModelsValidation()
        {
            RuleFor(t => t.AccessToken).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(t => t.RefreshToken).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
