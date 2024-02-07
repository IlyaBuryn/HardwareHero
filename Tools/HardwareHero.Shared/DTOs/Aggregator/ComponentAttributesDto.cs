namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class ComponentAttributesDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public ComponentDto? Component { get; set; }
    }
}
