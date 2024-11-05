using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Repositories.EF
{
    public class EFQueryRepositoryAsync<T> : EFBaseRepositoryAsync<T>, IQueryRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFQueryRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<DataAnswer<IQueryable<T>>> FindAsync(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                if (predicate == null)
                {
                    return new(await Task.FromResult(_dbSet.AsQueryable()));
                }

                var query = ApplyIncludeProps(includeProperties);
                query = query.Where(predicate).AsQueryable();

                return new(await Task.FromResult(query));
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async Task<PagedDataAnswer<T>> FindPagedAsync(Func<T, bool>? predicate, IPaginable filter,
            params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                if (filter.IsWrongPageOptions())
                {
                    return new(new PageOptionsValidationException());
                }

                var query = ApplyIncludeProps(includeProperties);
                int skip = (int)((filter.PageNumber - 1) * filter.PageSize);
                var total = (int)Math.Ceiling((double)query.Count() / filter.PageSize);

                if (predicate == null)
                {
                    return new(
                        await Task.FromResult(query.Skip(skip).Take((int)filter.PageSize)),
                        (int)filter.PageNumber,
                        (int)filter.PageSize,
                        total);
                }

                return new(
                    await Task.FromResult(
                        query.Where(predicate).Skip(skip).Take((int)filter.PageSize)),
                    (int)filter.PageNumber,
                    (int)filter.PageSize,
                    total);
            }
            catch (Exception ex) 
            {
                return new(ex);
            }
        }
    }
}
