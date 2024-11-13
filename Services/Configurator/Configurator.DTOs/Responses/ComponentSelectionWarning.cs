namespace Configurator.DTOs.Responses
{
    public class ComponentSelectionWarning
    {
        public int StatusCode { get; set; }
        public List<string> RelatedObjectNames { get; set; }
        public List<string> ProblemComponentTypes { get; set; }
    }
}
