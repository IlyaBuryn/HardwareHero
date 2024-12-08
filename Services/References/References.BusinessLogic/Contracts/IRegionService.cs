using References.DTOs.Domain;
using References.DTOs.Messages;

namespace References.BusinessLogic.Contracts
{
    public interface IRegionService
    {
        Task<Guid?> AddRegionAsync(RegionDto regionToAdd);
        Task<bool> UpdateRegionAsync(RegionDto regionToUpdate);
        Task<IEnumerable<RegionDto>?> GetRegionsAsync();
        Task<RegionDto?> FindRegionAsync(RegionRequestMessage filter);
    }
}
