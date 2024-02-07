namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class ComponentImagesDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public string Image { get; set; }
        public ComponentDto? Component { get; set; }

        public byte[] ImageData { get; set; } // base64
    }
}
