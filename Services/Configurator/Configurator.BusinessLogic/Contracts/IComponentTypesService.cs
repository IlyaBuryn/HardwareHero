namespace Configurator.BusinessLogic.Contracts
{
    public interface IComponentTypesService
    {
        Task<Guid?> AddComponentTypeSignsAsync(ComponentTypeSignsDto signsToCreate);
        Task<bool> UpdateComponentTypeSignsAsync(ComponentTypeSignsDto signsToUpdate);
        Task<ComponentTypeSignsDto?> GetComponentTypeSignsByNameAsync(string name);
        Task<List<ComponentTypeSignsDto>> SeedDatabaseAsync(List<ComponentTypeSigns> signsListToAdd);
        Task<List<ComponentTypeSignsDto>> GetComponentTypeSignsAsync();
    
    }
}
