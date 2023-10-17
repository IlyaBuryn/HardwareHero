using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.EF
{
    public class EFCrudRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public DbContext GetDbContext() => _dbContext;

        public EFCrudRepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<Guid> CreateEntityAsync([NotNull] T entityToCreate)
        {
            await _dbSet.AddAsync(entityToCreate);
            await _dbContext.SaveChangesAsync();

            return entityToCreate.Id;
        }

        public async Task<bool> UpdateEntityAsync([NotNull] T entityToUpdate)
        {
            _dbContext.Update(entityToUpdate);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> RemoveEntityAsync([NotNull] Guid entityId)
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

        public async Task<T?> GetOneEntityAsync([NotNull] Guid entityId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<T?> GetOneEntityAsync([NotNull] Guid entityId, IncludeProperties<T> includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await query.FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression )
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T> includeProperties)
        {

            IQueryable<T> query = GetIncludeProperties(includeProperties);
            query = query.Where(expression);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync(IncludeProperties<T> includeProperties = null)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync([NotNull] Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(expression);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T?>> GetManyEntitiesAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T> includeProperties)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);
            query = query.Where(expression);

            return await Task.FromResult(query);
        }

        protected IQueryable<T> GetIncludeProperties(IncludeProperties<T> includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties == null)
            {
                return query;
            }

            if (includeProperties.IsAllIncludes)
            {
                foreach (var navigationProperty in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                {
                    query = query.Include(navigationProperty.Name);
                }
            }
            else if (!includeProperties.IncludeExpressions.IsNullOrEmpty())
            {
                foreach (var includeProperty in includeProperties.IncludeExpressions)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query;
        }
    }
}
