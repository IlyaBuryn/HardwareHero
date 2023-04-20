namespace HardwareHero.Services.Shared.DTOs.Configurator
{
    public class ComponentTypeSpecificationDto
    {
        public string? Manufacturer { get; set; }
        public string[]? Series { get; set; }
        public string? CPUManufacturer { get; set; }
        public string[]? FormFactor { get; set; }
        public string[]? Types { get; set; }
    }
}