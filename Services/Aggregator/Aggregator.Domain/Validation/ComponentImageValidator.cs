using Aggregator.Domain.Components.DTO;
using FluentValidation;

namespace Aggregator.Domain.Validation
{
    public class ComponentImageValidator 
        : AbstractValidator<ComponentImageDto>
    {
        public ComponentImageValidator()
        {
            // ---
        }
    }
}
