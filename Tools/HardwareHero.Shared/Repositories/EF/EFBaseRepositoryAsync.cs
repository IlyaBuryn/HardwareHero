using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Repositories.EF
{
    public class EFBaseRepositoryAsync<T> : IBaseRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFBaseRepositoryAsync(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<DataAnswer<T>> CreateEntityAsync(T entityToCreate)
        {
            if (entityToCreate == null)
            {
                return new(new ArgumentNullException(nameof(entityToCreate)));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityToCreate.Id });
            if (entity == null)
            {
                await _dbSet.AddAsync(entityToCreate);
                var result = await _dbContext.SaveChangesAsync();

                return new(entity);
            }

            return new(new AlreadyExistException(entity));
        }

        public async Task<DataAnswer<T>> UpdateEntityAsync(T entityToUpdate)
        {
            if (entityToUpdate == null)
            {
                return new(new ArgumentNullException(nameof(entityToUpdate)));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityToUpdate.Id });
            if (entity != null)
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(entity);
                var result = await _dbContext.SaveChangesAsync();

                return new(entity);
            }

            return new(new NotFoundException(nameof(entity)));
        }

        public async Task<DataAnswer<T>> RemoveEntityAsync(Guid entityId)
        {
            if (entityId == Guid.Empty)
            {
                return new(new ArgumentNullException(nameof(entityId)));
            }

            var entity = await _dbContext.FindAsync<T>(new object[] { entityId });
            if (entity != null)
            {
                _dbContext.Remove(entity);
                var result = await _dbContext.SaveChangesAsync();

                return new(entity);
            }

            return new(new NotFoundException(nameof(entity)));
        }

        public async Task<DataAnswer<T>> FindEntityAsync(Guid entityId, 
            params Expression<Func<T, object>>[] includeProperties) => 
                await FindEntityAsync(x => x.Id == entityId, includeProperties);

        public async Task<DataAnswer<T>> FindEntityAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            if (predicate == null)
            {
                return new(new ArgumentNullException(nameof(predicate)));
            }

            var query = ApplyIncludeProps(includeProperties);

            return new(await query.AsNoTracking().FirstOrDefaultAsync(predicate));
        }


        public async Task<DataAnswer<IEnumerable<T>>> FindAllEntitiesAsync(
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = ApplyIncludeProps(includeProperties);
            return new(await Task.FromResult(query));
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var query = predicate != null ? _dbSet.Where(predicate) : _dbSet;

            return await Task.FromResult(_dbSet.Count());
        }

        protected IQueryable<T> ApplyIncludeProps(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
    }
}
