namespace Identity.Api.Records.Validation
{
    public class TokenRequestModelsValidation : AbstractValidator<RequestModels.TokenRequest>
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
