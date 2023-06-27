using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComputerServiceValidator : AbstractValidator<ComputerServiceDto>
    {
        public ComputerServiceValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Name).MaximumLength(256)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.Description).MaximumLength(1024)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
