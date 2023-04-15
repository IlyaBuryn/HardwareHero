﻿using FluentValidation;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentValidator : AbstractValidator<ComponentDto>
    {
        public ComponentValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("{PropertyName} is required!");

            RuleFor(c => c.Name).MaximumLength(256)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.Description).MaximumLength(1024)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.InitialPrice).GreaterThanOrEqualTo(0m)
                .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue}.");
        }
    }
}
