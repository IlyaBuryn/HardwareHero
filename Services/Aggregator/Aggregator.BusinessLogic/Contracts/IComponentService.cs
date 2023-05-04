using HardwareHero.Services.Shared.DTOs;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentService
    {
        Task<List<ComponentDto?>> GetComponentsAsPageAsync(int pageNumber, int pageSize, string specificationFilter);
        Task<ComponentDto?> GetComponentByIdAsync(Guid componentId);
        Task<Guid?> AddComponentAsync(ComponentDto componentToAdd);
        Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate);
        Task<bool> RemoveComponentAsync(Guid componentId);
        Task<decimal> GetComponentAvgMarkAsync(Guid componentId);
    }
}
