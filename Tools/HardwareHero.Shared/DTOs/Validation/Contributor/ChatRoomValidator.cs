using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Contributor;

namespace HardwareHero.Shared.DTOs.Validation.Contributor
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
