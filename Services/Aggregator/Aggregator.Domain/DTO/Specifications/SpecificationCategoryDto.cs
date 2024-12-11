namespace Aggregator.Specifications.DTO
{
    public class SpecificationCategoryDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public int Priority { get; set; }
    }
}
