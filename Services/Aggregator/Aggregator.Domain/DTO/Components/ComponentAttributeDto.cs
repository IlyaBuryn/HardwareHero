namespace Aggregator.Domain.Components.DTO
{
    public class ComponentAttributeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ComponentId { get; set; }
        public Guid SpecificationAttributeId { get; set; }
        public string? Value { get; set; }
    }
}
