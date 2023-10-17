using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Responses;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentService
    {
        Task<Guid?> AddComponentAsync(ComponentDto componentToAdd);
        Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate);
        Task<bool> RemoveComponentAsync(Guid componentId);

        Task<Guid[]> AddComponentsFromJsonAsync(string jsonData);

        Task<ComponentDto?> GetComponentByIdAsync(Guid componentId);
        Task<List<ComponentDto?>> GetComponentsByIdsAsync(Guid[] componentsIds);
        Task<PageResponse<ComponentDto?>> GetComponentsAsPageAsync(AggregatorFilter filter);
    }
}
