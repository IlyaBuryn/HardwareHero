using HardwareHero.Services.Shared.Models;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<Guid> CreateEntityAsync(T entityToCreate);
        Task<bool> UpdateEntityAsync(T entityToUpdate);
        Task<bool> RemoveEntityAsync(Guid entityId);
        Task<T?> GetOneEntityAsync(params Expression<Func<T, object>>[]? includeProperties);
        Task<T?> GetOneEntityAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties);
        Task<IQueryable<T?>> GetManyEntitiesAsync(params Expression<Func<T, object>>[]? includeProperties);
        Task<IQueryable<T?>> GetManyEntitiesAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties);
        Task<IQueryable<T?>> GetManyEntitiesAsync(Func<T, object>? orderBy,
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties);
    }
}
