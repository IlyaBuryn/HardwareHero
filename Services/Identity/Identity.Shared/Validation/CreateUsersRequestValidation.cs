using FluentValidation;
using HardwareHero.Shared.Constants;
using static Identity.Shared.Requests.IdentityRequestRecords;

namespace Identity.Shared.Validation
{
    public class CreateUsersRequestValidation : AbstractValidator<CreateUserRequest>
    {
        public CreateUsersRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }

    public class UpdateUsersRequestValidation : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUsersRequestValidation()
        {
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
