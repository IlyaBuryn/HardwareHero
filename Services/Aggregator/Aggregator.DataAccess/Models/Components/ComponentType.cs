using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Components
{
    public class ComponentType : BaseEntity
    {
        public string Name { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public bool AccessibleForConfigurator { get; set; } = false;
    }
}
