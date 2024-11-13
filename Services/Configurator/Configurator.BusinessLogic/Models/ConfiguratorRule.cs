namespace Configurator.BusinessLogic.Models
{
    // TODO: Don't use
    public class ConfiguratorRule
    {
        public string ComponentType1 { get; set; }
        public string AttributeName1 { get; set; }
        public string ComponentType2 { get; set; }
        public string AttributeName2 { get; set; }
        public int ProblemLevel { get; set; } = 1;

        public bool IsSymmetric { get; set; } = true;
    }
}
