namespace HardwareHero.Shared.Models.Configurator
{
    public class ConfiguratorComponentAttribute
    {
        public string Key{ get; set; }
        public string Value { get; set; }
        public string Operation { get; set; } = "value";
    }
}
