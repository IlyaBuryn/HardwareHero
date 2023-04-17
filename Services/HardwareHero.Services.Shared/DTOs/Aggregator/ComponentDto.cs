namespace HardwareHero.Services.Shared.DTOs
{
    public class ComponentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public string Specifications { get; set; }
        public decimal InitialPrice { get; set; }
    }
}
