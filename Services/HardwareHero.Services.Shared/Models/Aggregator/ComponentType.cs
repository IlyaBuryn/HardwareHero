using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Aggregator
{
    public class ComponentType : BaseEntity
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}
