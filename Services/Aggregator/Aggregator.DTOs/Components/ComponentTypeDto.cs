namespace Aggregator.DTOs.Components
{
    public class ComponentTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public bool AccessibleForConfigurator { get; set; }
    }
}
