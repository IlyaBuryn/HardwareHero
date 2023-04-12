using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.DTOs.Validation.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Validation.Contributor
{
    [Validator(typeof(ChatMessageDto))]
    public class ChatMessageValidator : AbstractValidator<ChatMessageDto>
    {
        public ChatMessageValidator()
        {
            RuleFor(c => c.Text).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.Text).MaximumLength(2048)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.SenderId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ChatRoomId).NotEmpty()
                .WithMessage("{PropertyName} is required.");
        }
    }
}
