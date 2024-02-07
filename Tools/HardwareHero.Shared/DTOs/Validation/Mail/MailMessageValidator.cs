using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Mail;

namespace HardwareHero.Shared.DTOs.Validation.Mail
{
    public class MailMessageValidator : AbstractValidator<MailMessageDto>
    {
        public MailMessageValidator()
        {
            RuleFor(x => x.MessageContent).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.MessageTitle).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.SenderId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(x => x.RecipientsEmailAddress).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
