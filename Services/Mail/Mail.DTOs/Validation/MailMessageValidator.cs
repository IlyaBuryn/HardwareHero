using FluentValidation;
using HardwareHero.Shared.Constants;
using Mail.DTOs.Mail;

namespace Mail.DTOs.Validation
{
    public class MailMessageValidator : AbstractValidator<MailMessageDto>
    {
        public MailMessageValidator()
        {
            RuleFor(x => x.Subject).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.Body).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.SenderId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.RecipientMailAddress).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
