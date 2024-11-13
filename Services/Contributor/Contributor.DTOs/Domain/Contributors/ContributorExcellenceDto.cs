using Contributor.DTOs.Domain.Currencies;
using Contributor.DTOs.Domain.Regions;
using Microsoft.AspNetCore.Http;

namespace Contributor.DTOs.Domain.Contributors
{
    public class ContributorExcellenceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? LogoName { get; set; }
        public IFormFile ImageData { get; set; }
        public string? Description { get; set; }
        public string Phone { get; set; }
        public string? MainWebLink { get; set; }
        public string? MainApiLink { get; set; }
        public Guid RegionId { get; set; }
        public Guid? CurrencyId { get; set; }

        public CurrencyDto? Currency { get; set; }
        public RegionDto? Region { get; set; }
    }
}
