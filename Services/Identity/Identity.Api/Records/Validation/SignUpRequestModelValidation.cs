namespace Identity.Api.Records.Validation
{
    public class SignUpRequestModelValidation : AbstractValidator<RequestModels.SignUpRequestModel>
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
