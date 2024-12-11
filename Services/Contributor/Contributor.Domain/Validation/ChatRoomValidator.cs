using Contributor.Domain.DTO.Chat;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Contributor.Domain.Validation
{
    public class ChatRoomValidator : AbstractValidator<ChatRoomDto>
    {
        public ChatRoomValidator()
        {
            RuleFor(c => c.Subject).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Subject).MaximumLength(ValidationValues.SubjectMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.TimeStamp).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
