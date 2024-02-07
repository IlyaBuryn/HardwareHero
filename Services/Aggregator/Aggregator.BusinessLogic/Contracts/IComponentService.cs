namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentService
    {
        Task<Guid?> AddComponentAsync(ComponentDto componentToAdd);
        Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate);
        Task<bool> RemoveComponentAsync(Guid componentId);

        Task<ComplexResponse> AddComponentsAsync(IEnumerable<ComponentDto> componentsToAdd);

        Task<ComponentDto?> GetComponentByIdAsync(Guid componentId);
        Task<List<ComponentDto?>> GetComponentsByIdsAsync(List<Guid> componentsIds);
        Task<PageResponse<ComponentDto?>> GetComponentsAsPageAsync(ComponentsFilter filter);
    }
}
