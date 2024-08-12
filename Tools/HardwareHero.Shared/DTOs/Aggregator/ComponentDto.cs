namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class ComponentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ComponentTypeId { get; set; }
        public ComponentTypeDto? ComponentType { get; set; }
        public ICollection<ComponentImagesDto>? ComponentImages { get; set; }
        public ICollection<ComponentAttributesDto>? ComponentAttributes { get; set; }
    }
}
