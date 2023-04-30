using HardwareHero.Services.Shared.Models;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface ISimpleRepositoryAsync<T> where T : BaseEntity
    {
        Task<Guid> CreateEntityAsync(T entityToCreate);
        Task<bool> UpdateEntityAsync(T entityToUpdate);
        Task<bool> RemoveEntityAsync(Guid entityId);
        Task<T?> GetOneEntityAsync(Expression<Func<T, bool>> expression);
        Task<IQueryable<T?>> GetManyEntitiesAsync(Expression<Func<T, bool>> expression);
    }
}
