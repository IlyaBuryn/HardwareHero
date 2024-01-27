﻿using FluentValidation;
using HardwareHero.Services.Shared.Constants;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentValidator : AbstractValidator<ComponentDto>
    {
        public ComponentValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Name).MaximumLength(ValidationValues.ComponentNameMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.Description).MaximumLength(ValidationValues.DescriptionMaxLength)
                .WithMessage(ValidationMessages.MaximumLength);

            RuleFor(c => c.ComponentTypeId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);
        }
    }
}
