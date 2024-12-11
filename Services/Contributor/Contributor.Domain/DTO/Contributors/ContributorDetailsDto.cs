using Microsoft.AspNetCore.Http;
using References.DTOs.Domain;

namespace Contributor.Domain.DTO.Contributors
{
    public class ContributorDetailsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? CompanyName { get; set; }
        public string? CompanyLogoUrl { get; set; }
        public IFormFile? CompanyLogoFile { get; set; }
        public string? CompanyPhone { get; set; }
        public Guid CompanyRegionId { get; set; }
        public Guid CompanyCurrencyId { get; set; }
        public Guid ContributorId { get; set; }

        public string? Description { get; set; }

        public string? ComponentsLink { get; set; }

        public RegionDto? Region { get; set; }
        public CurrencyDto? Currency { get; set; }
    }
}
