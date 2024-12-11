namespace Aggregator.Domain.Components.DTO
{
    public class ComponentTypeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public bool AccessibleForConfigurator { get; set; }
    }
}
