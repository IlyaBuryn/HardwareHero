using HardwareHero.Shared.Repositories.Answers;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface IBaseRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        Task<DataAnswer<T>> CreateEntityAsync(T entityToCreate);
        Task<DataAnswer<T>> UpdateEntityAsync(T entityToUpdate);
        Task<DataAnswer<T>> RemoveEntityAsync(Guid entityId);
        Task<DataAnswer<T>> FindEntityAsync(Guid entityId,
            params Expression<Func<T, object>>[] includeProperties);
        Task<DataAnswer<T>> FindEntityAsync(Expression<Func<T, bool>> predicate, 
            params Expression<Func<T, object>>[] includeProperties);
        Task<DataAnswer<IEnumerable<T>>> FindAllEntitiesAsync(
            params Expression<Func<T, object>>[] includeProperties);
    }
}
