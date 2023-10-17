using FluentValidation;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentTypesValidator : AbstractValidator<ComponentTypeDto>
    {
        public ComponentTypesValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Name).MaximumLength(32)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.FullName).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.FullName).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Description).MaximumLength(1024)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
