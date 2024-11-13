namespace Aggregator.DTOs.Specifications
{
    public class SpecificationAttributeDto
    {
        public Guid Id { get; set; }
        public Guid ComponentTypeId { get; set; }
        public Guid? SpecificationFilterTypeId { get; set; }
        public Guid SpecificationCategoryId { get; set; }
        public string Key { get; set; }
        public string? Unit { get; set; }
    }
}
