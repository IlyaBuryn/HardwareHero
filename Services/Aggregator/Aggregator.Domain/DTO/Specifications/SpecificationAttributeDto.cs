namespace Aggregator.Specifications.DTO
{
    public class SpecificationAttributeDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ComponentTypeId { get; set; }
        public Guid? SpecificationFilterTypeId { get; set; }
        public Guid SpecificationCategoryId { get; set; }
        public string? AttributeKey { get; set; }
        public string? Unit { get; set; }
    }
}
