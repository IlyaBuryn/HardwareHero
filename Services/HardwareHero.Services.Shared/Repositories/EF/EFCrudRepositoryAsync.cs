using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.EF
{
    public class EFCrudRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFCrudRepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<Guid> CreateEntityAsync(T entityToCreate)
        {
            await _dbSet.AddAsync(entityToCreate);
            await _dbContext.SaveChangesAsync();
            return entityToCreate.Id;
        }

        public async Task<bool> UpdateEntityAsync(T entityToUpdate)
        {
            _dbContext.Update(entityToUpdate);
            if (entityToUpdate != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> RemoveEntityAsync(Guid entityId)
        {
            T? entity = await _dbContext.FindAsync<T>(new object[] { entityId });
            if (entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<T?> GetOneEntityAsync(params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetOneEntityAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync(params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync(Func<T, object>? orderBy,
            Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy).AsQueryable();
            }

            return await Task.FromResult(query);
        }

        protected IQueryable<T> GetIncludeProperties(params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
    }
}
