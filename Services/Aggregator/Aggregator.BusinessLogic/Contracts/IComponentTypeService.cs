namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentTypeService
    {
        Task<Guid?> AddComponentTypeAsync(ComponentTypeDto componentTypeToAdd);
        Task<bool> UpdateComponentTypeAsync(ComponentTypeDto componentTypeToUpdate);
        Task<bool> RemoveComponentTypeAsync(Guid typeId);

        Task<List<ComponentTypeDto?>> GetComponentTypesAsync();
    }
}
