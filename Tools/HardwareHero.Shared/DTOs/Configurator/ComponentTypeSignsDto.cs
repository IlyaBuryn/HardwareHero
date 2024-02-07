namespace HardwareHero.Shared.DTOs.Configurator
{
    public class ComponentTypeSignsDto
    {
        public Guid Id { get; set; }
        public string[] ComponentNames { get; set; }
        public string Description { get; set; }
        public ComponentTypeSpecificationDto[] Specifications { get; set; }
        public string Image { get; set; }
    }
}
