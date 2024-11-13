using Aggregator.DTOs.Components;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentService
    {
        Task<Guid?> AddComponentAsync(ComponentDto componentToAdd);
        Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate);
        Task<bool> RemoveComponentAsync(Guid componentId, bool deactivate = true);

        Task<CreationOfManyResponse> AddComponentsAsync(IEnumerable<ComponentDto> componentsToAdd);

        Task<ComponentDto?> GetComponentByIdAsync(Guid componentId);
        Task<IQueryable<ComponentDto?>> GetComponentsByIdsAsync(List<Guid> componentsIds);
        Task<PageResponse<ComponentDto>?> GetComponentsPageAsync(ComponentsFilter filter);

        // Task<bool> RefreshComponentMetrics(Guid componentId);
    }
}
