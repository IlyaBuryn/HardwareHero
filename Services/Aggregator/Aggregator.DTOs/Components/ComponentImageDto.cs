using Microsoft.AspNetCore.Http;

namespace Aggregator.DTOs.Components
{
    public class ComponentImageDto
    {
        public Guid Id { get; set; }
        public Guid? ComponentId { get; set; }
        public Guid? RevokedId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public int Index { get; set; }
        public IFormFile ImageData { get; set; }
    }
}
