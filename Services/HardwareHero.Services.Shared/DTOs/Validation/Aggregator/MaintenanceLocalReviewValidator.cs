﻿using FluentValidation;
using HardwareHero.Services.Shared.Constants;
using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace HardwareHero.Services.Shared.DTOs.Validation.Aggregator
{
    public class MaintenanceLocalReviewValidator : AbstractValidator<MaintenanceLocalReviewDto>
    {
        public MaintenanceLocalReviewValidator()
        {
            RuleFor(c => c.UserId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.MaintenanceId).NotEmpty()
                .WithMessage(ValidationMessages.IsRequired);

            RuleFor(c => c.Rating).GreaterThan(ValidationValues.RatingFromNotIncluding)
                .WithMessage(ValidationMessages.GreaterThan);

            RuleFor(c => c.Rating).LessThan(ValidationValues.RatingToNotIncluding)
                .WithMessage(ValidationMessages.LessThan);
        }
    }
}
