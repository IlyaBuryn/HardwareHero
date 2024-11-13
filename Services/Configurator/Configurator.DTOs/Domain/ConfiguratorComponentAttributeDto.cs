namespace Configurator.DTOs.Domain
{
    public class ConfiguratorComponentAttributeDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Operation { get; set; } = "value";
    }
}
