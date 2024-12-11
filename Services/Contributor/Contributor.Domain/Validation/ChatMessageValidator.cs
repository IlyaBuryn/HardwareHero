using Contributor.Domain.DTO.Chat;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ChatMessageValidator : AbstractValidator<ChatMessageDto>
    {
        public ChatMessageValidator()
        {
            RuleFor(c => c.Text).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Text).MaximumLength(ValidationValues.TextMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.SenderId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ChatRoomId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
