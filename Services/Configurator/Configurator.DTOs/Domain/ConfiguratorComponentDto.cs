namespace Configurator.DTOs.Domain
{
    public class ConfiguratorComponentDto
    {
        public Guid ComponentId { get; set; }
        public string ComponentType { get; set; }
        public List<ConfiguratorComponentAttributeDto> Attributes { get; set; }
        public bool IsProblem { get; set; }
    }
}
