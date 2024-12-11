namespace Aggregator.Specifications.DTO
{
    public class SpecificationFilterTypeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
    }
}
