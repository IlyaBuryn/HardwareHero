using References.DTOs.Domain;
using References.DTOs.Messages;

namespace References.BusinessLogic.Contracts
{
    public interface IRegionService
    {
        Task<RegionDto> AddRegionAsync(RegionDto regionToAdd);
        Task<RegionDto> UpdateRegionAsync(RegionDto regionToUpdate);
        Task<IEnumerable<RegionDto>?> GetRegionsAsync();
        Task<RegionDto?> FindRegionAsync(RegionRequestMessage filter);
    }
}
