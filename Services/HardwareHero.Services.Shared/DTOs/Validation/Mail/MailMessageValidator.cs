using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Mail;

namespace HardwareHero.Services.Shared.DTOs.Validation.Mail
{
    public class MailMessageValidator : AbstractValidator<MailMessageDto>
    {
        public MailMessageValidator()
        {
            RuleFor(x => x.MessageContent).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(x => x.MessageTitle).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(x => x.SenderId).NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}
