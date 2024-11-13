using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Repositories.EF
{
    public class EFSpecificRepositoryAsync<T> : EFBaseRepositoryAsync<T>, ISpecificRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFSpecificRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<DataAnswer<T>> UpdateEntityValueAsync<S>(
            Expression<Func<T, bool>> predicate,
            Action<T> updateAction) where S : struct
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                return new(new NotFoundException(typeof(T).Name));
            }

            updateAction(entity);

            _dbContext.SaveChanges();

            return new(entity);
        }
    }
}
