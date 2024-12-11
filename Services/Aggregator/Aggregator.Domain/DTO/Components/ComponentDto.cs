namespace Aggregator.Domain.Components.DTO
{
    public class ComponentDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public Guid ComponentTypeId { get; set; }
        public Guid? ComponentMetricId { get; set; }
        public ComponentTypeDto? ComponentType { get; set; }
        public ComponentMetricDto? componentMetric { get; set; }
        public ICollection<ComponentImageDto>? ComponentImages { get; set; }
        public ICollection<ComponentAttributeDto>? ComponentAttributes { get; set; }
    }
}
