using HardwareHero.Shared.Models.Configurator;

namespace HardwareHero.Shared.Responses
{
    public class ComponentSelectionResponse
    {
        public Guid ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string Description { get; set; }
        public List<ConfiguratorComponentAttribute>? Attributes { get; set; }
    }

    public class ComponentSelectionWarning
    {
        public int StatusCode { get; set; }
        public List<string> RelatedObjectNames { get; set; }
        public List<string> ProblemComponentTypes { get; set; }

    }
}
