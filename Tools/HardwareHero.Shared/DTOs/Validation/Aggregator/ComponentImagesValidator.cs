using FluentValidation;
using HardwareHero.Shared.Constants;
using HardwareHero.Shared.DTOs.Aggregator;

namespace HardwareHero.Shared.DTOs.Validation.Aggregator
{
    public class ComponentImagesValidator : AbstractValidator<ComponentImagesDto>
    {
        public ComponentImagesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Image).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ImageData).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
