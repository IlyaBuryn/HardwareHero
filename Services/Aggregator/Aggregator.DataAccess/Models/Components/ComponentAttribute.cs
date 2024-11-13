using Aggregator.DataAccess.Models.Specifications;
using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentAttribute : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public Guid SpecificationAttributeId { get; set; }
        public string? Value { get; set; }

        public virtual Component? Component { get; set; }
        public virtual SpecificationAttribute? SpecificationAttribute { get; set; }
    }
}
