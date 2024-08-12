namespace Configurator.BusinessLogic.Contracts
{
    public interface IAssemblyService
    {
        Task<List<StoredAssemblyDto?>> GetAssembliesByUserIdAsync(Guid userId);
        Task<Guid?> SaveAssemblyAsync(StoredAssemblyDto assemblyToAdd);
        Task<bool> UpdateAssemblyAsync(StoredAssemblyDto assemblyToUpdate);
        Task<bool> RemoveAssemblyAsync(Guid assemblyId);
    }
}
