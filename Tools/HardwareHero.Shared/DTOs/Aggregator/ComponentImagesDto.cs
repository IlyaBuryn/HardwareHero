using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace HardwareHero.Shared.DTOs.Aggregator
{
    public class ComponentImagesDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public string Image { get; set; }
        public IFormFile ImageData { get; set; }

        [JsonIgnore]
        public ComponentDto? Component { get; set; }

    }
}
