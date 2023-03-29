﻿using FluentValidation;
using HardwareHero.Services.Shared.DTOs.Validation.Attributes;

namespace HardwareHero.Services.Shared.DTOs.Validation
{
    [Validator(typeof(ComponentReviewDto))]
    public class ComponentReviewValidator : AbstractValidator<ComponentReviewDto>
    {
        public ComponentReviewValidator()
        {
            RuleFor(c => c.Name).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");

            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage("{PropertyName} is required.");

            RuleFor(c => c.ContributorName).MaximumLength(128)
                .WithMessage("{PropertyName} must be less than {MaxLength} characters!");
        }
    }
}
