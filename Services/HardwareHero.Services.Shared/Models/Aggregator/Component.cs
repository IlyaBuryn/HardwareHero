using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class Component : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ComponentTypeId { get; set; }

        public virtual ComponentType? ComponentType { get; set; }
        public virtual ICollection<ComponentImages>? ComponentImages { get; set; }
        public virtual ICollection<ComponentAttributes>? ComponentAttributes { get; set; }
    }
}
