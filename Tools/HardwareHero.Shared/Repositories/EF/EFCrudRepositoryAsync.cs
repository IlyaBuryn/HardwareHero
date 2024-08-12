using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HardwareHero.Shared.Repositories.EF
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
            if (entityToCreate == null)
            {
                throw new ArgumentNullException(nameof(entityToCreate));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityToCreate.Id });
            if (entity == null)
            {
                await _dbSet.AddAsync(entityToCreate);
                var result = await _dbContext.SaveChangesAsync();

                return entityToCreate.Id;
            }

            return Guid.Empty;
        }

        public async Task<bool> UpdateEntityAsync([NotNull] T entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException(nameof(entityToUpdate));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityToUpdate.Id });
            if (entity != null)
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(entityToUpdate);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }

            return false;
        }

        public async Task<bool> RemoveEntityAsync([NotNull] Guid entityId)
        {
            if (entityId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityId });
            if (entity != null)
            {
                _dbContext.Remove(entity);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }

            return false;
        }

        public virtual async Task<T?> GetOneEntityAsync([NotNull] Guid entityId, IncludeProperties<T>? includeProperties = null)
        {
            if (entityId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(entityId));
            }

            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public virtual async Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression )
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<T?> GetOneEntityAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T> includeProperties)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            IQueryable<T> query = GetIncludeProperties(includeProperties);
            query = query.AsNoTracking().Where(expression);

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }


        public virtual async Task<IQueryable<T?>> GetManyEntitiesAsync(IncludeProperties<T>? includeProperties = null)
        {
            IQueryable<T> query = GetIncludeProperties(includeProperties);

            return await Task.FromResult(query);
        }

        public virtual async Task<IQueryable<T?>> GetManyEntitiesAsync([NotNull] Expression<Func<T, bool>> expression, IncludeProperties<T>? includeProperties = null)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            IQueryable<T> query = GetIncludeProperties(includeProperties);
            query = query.Where(expression);

            return await Task.FromResult(query);
        }

        protected IQueryable<T> GetIncludeProperties(IncludeProperties<T>? includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (includeProperties == null)
            {
                return query;
            }

            if (includeProperties.IsAllIncludes)
            {
                var entityType = _dbContext.Model.FindEntityType(typeof(T));
                if (entityType != null)
                {
                    foreach (var navigationProperty in entityType.GetNavigations())
                    {
                        query = query.Include(navigationProperty.Name);
                    }
                }
            }
            else if (includeProperties.IncludeExpressions != null && includeProperties.IncludeExpressions.Count() != 0)
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
