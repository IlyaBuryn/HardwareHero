using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Mail;

namespace HardwareHero.Services.Shared.DTOs.Validation.Mail
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
