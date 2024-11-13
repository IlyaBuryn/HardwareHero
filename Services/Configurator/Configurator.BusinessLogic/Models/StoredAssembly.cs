using HardwareHero.Shared.Models;

namespace Configurator.BusinessLogic.Models
{
    public class StoredAssembly : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public List<ConfiguratorComponent> SelectedComponents { get; set; } = new List<ConfiguratorComponent>();
    }
}
