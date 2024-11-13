using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Components
{
    public class Component : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public Guid ComponentTypeId { get; set; }
        public Guid? ComponentMetricId { get; set; }

        public virtual ComponentType? ComponentType { get; set; }
        public virtual ComponentMetric? ComponentMetric { get; set; }
        public virtual ICollection<ComponentImage>? ComponentImages { get; set; }
        public virtual ICollection<ComponentAttribute>? ComponentAttributes { get; set; }
    }
}
