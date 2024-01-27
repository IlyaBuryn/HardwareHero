﻿using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class ComponentGlobalReviewValidator : AbstractValidator<ComponentGlobalReviewDto>
    {
        public ComponentGlobalReviewValidator()
        {
            RuleFor(c => c.AuthorName).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ComponentId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.ContributorId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Rating).GreaterThan(ValidationValues.RatingFromNotIncluding)
                .WithMessage(ValidationMessages.GreaterThan);

            RuleFor(c => c.Rating).LessThan(ValidationValues.RatingToNotIncluding)
                .WithMessage(ValidationMessages.LessThan);
        }
    }
}
