namespace Aggregator.DTOs.Components
{
    public class ComponentAttributeDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public Guid SpecificationAttributeId { get; set; }
        public string? Value { get; set; }
    }
}
