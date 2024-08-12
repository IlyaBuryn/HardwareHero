namespace HardwareHero.Shared.Models.Configurator
{
    public class ConfiguratorRule
    {
        public string TargetComponentType { get; set; }
        public string TargetAttributeName { get; set; }
        public string SourceComponentType { get; set; }
        public string SourceAttributeName { get; set; }
        public int ProblemLevel { get; set; }
    }
}
