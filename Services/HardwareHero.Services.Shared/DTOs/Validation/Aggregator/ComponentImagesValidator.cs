using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentImagesValidator : AbstractValidator<ComponentImagesDto>
    {
        public ComponentImagesValidator()
        {
            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Image).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.ImageData).NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}
