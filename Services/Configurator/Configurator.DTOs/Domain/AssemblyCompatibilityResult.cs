namespace Configurator.DTOs.Domain
{
    public class AssemblyCompatibilityResult
    {
        public bool IsCompatible { get; set; } = true;
        public List<string> Problems { get; set; } = new List<string>();
    }
}
