using Microsoft.AspNetCore.Http;
using Storage.DTOs.Contracts;

namespace Aggregator.Domain.Components.DTO
{
    public class ComponentImageDto : IFileNameBuilder
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ComponentId { get; set; }
        public Guid? RevokedId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        public int Index { get; set; }
        public IFormFile? ImageData { get; set; }

        public string BuildFileName()
            => string.Join("_", Id, Index);
    }
}
