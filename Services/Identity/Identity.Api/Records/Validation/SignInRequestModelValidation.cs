namespace Identity.Api.Records.Validation
{
    public class SignInRequestModelValidation : AbstractValidator<RequestModels.SignInRequestModel>
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
