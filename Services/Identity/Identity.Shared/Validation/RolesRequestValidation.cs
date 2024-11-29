using FluentValidation;
using HardwareHero.Shared.Constants;
using static Identity.Shared.Requests.UsersRequestRecords;

namespace Identity.Shared.Validation
{
    public class RolesRequestValidation : AbstractValidator<RolesRequest>
    {
        public RolesRequestValidation()
        {
            RuleFor(s => s.Roles).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }

    public class UserRolesRequestValidation : AbstractValidator<UserRolesRequest>
    {
        public UserRolesRequestValidation()
        {
            RuleFor(s => s.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(s => s.Roles).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
