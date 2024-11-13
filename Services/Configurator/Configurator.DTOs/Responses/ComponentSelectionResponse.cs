using Configurator.DTOs.Domain;

namespace Configurator.DTOs.Responses
{
    public class ComponentSelectionResponse
    {
        public Guid ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public List<ConfiguratorComponentAttribute>? Attributes { get; set; }
    }
}
