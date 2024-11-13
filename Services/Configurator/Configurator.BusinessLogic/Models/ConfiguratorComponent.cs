using Configurator.DTOs.Domain;
using HardwareHero.Shared.Models;

namespace Configurator.BusinessLogic.Models
{
    // TODO: Don't Use
    public class ConfiguratorComponent : BaseEntity
    {
        public string ComponentType { get; set; }
        public List<ConfiguratorComponentAttribute> Attributes { get; set; }
    }
}
