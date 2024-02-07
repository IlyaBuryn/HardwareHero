using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
{
    public class ChatMessageValidator : AbstractValidator<ChatMessageDto>
    {
        public ChatMessageValidator()
        {
            RuleFor(c => c.Text).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.IsEdited).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Text).MaximumLength(ValidationValues.TextMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Timestamp).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.SenderId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ChatRoomId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
