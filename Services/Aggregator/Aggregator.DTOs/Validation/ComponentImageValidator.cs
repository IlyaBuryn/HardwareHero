using Aggregator.DTOs.Components;
using FluentValidation;
using HardwareHero.Shared.Constants;

namespace Aggregator.DTOs.Validation
{
    public class ComponentImageValidator : AbstractValidator<ComponentImageDto>
    {
        public ComponentImageValidator()
        {
            // ---
        }
    }
}
