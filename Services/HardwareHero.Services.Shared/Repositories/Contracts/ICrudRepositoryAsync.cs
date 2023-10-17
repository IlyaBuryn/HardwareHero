using HardwareHero.Services.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<Guid> CreateEntityAsync([NotNull] T entityToCreate);
        Task<bool> UpdateEntityAsync([NotNull] T entityToUpdate);
        Task<bool> RemoveEntityAsync([NotNull] Guid entityId);

        Task<T?> GetOneEntityAsync([NotNull] Guid entityId);
        Task<T?> GetOneEntityAsync([NotNull] Guid entityId, IncludeProperties<T> includeProperties);
        Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression);
        Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T> includeProperties);

        Task<IQueryable<T?>> GetManyEntitiesAsync(IncludeProperties<T> includeProperties = null);
        Task<IQueryable<T?>> GetManyEntitiesAsync([NotNull] Expression<Func<T, bool>> expression);
        Task<IQueryable<T?>> GetManyEntitiesAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T> includeProperties);

        DbContext GetDbContext();

        //Task<T?> GetOneEntityAsync(params Expression<Func<T, object>>[]? includeProperties);
        //Task<T?> GetOneEntityAsync(
        //    Expression<Func<T, bool>> expression,
        //    params Expression<Func<T, object>>[]? includeProperties);
        //Task<IQueryable<T?>> GetManyEntitiesAsync(
        //    params Expression<Func<T, object>>[]? includeProperties);
        //Task<IQueryable<T?>> GetManyEntitiesAsync(
        //    Expression<Func<T, bool>> expression,
        //    params Expression<Func<T, object>>[]? includeProperties);
        //Task<IQueryable<T?>> GetManyEntitiesAsync(
        //    Func<T, object>? orderBy,
        //    Expression<Func<T, bool>> expression,
        //    params Expression<Func<T, object>>[]? includeProperties);
    }
}
