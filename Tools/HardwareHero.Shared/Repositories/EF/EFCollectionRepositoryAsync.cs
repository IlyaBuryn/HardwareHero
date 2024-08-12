using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HardwareHero.Shared.Repositories.EF
{
    public class EFCollectionRepositoryAsync<T> : EFCrudRepositoryAsync<T>, ICollectionRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFCollectionRepositoryAsync(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?>? query, [NotNull] IPaginable filter)
        {
            if (query == null)
            {
                query = Enumerable.Empty<T?>().AsQueryable();

                return query;
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (filter.IsWrongPageOptions())
            {
                throw new PageOptionsValidationException();
            }

            int skip = (int)((filter.PageNumber - 1) * filter.PageSize);
            IQueryable<T?> result = query.Skip(skip).Take((int)filter.PageSize);

            return await result.ToListAsync();
        }

        public async Task<int> GetTotalPageCountAsync(IQueryable<T?>? query, [NotNull] IPaginable filter)
        {
            if (query == null)
            {
                return 0;
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (filter.IsWrongPageOptions())
            {
                throw new PageOptionsValidationException();
            }

            var result = (int)Math.Ceiling((double)query.Count() / filter.PageSize);

            return await Task.FromResult(result);
        }

        public async Task<PageResponse<T?>> GetMappedPageAsync(IQueryable<T?>? query, [NotNull] IPaginable filter)
        {
            var items = await GetPageAsync(query, filter);
            var pageTotal = await GetTotalPageCountAsync(query, filter);

            var pageItems = items.ToList();
            
            return new PageResponse<T?>
            {
                Items = pageItems,
                TotalPages = (uint)pageTotal,
                CurrentPageSize = (uint)filter.PageSize,
                CurrentPageNumber = (uint)filter.PageNumber,
            };
        }

        public async Task<PageResponse<object?>> GetObjectPageAsync(IQueryable<object?>? query, [NotNull] IPaginable filter)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (filter.IsWrongPageOptions())
            {
                throw new PageOptionsValidationException();
            }

            int skip = (int)((filter.PageNumber - 1) * filter.PageSize);
            IQueryable<object?> items = query.Skip(skip).Take((int)filter.PageSize);

            var pageTotal = (int)Math.Ceiling((double)query.Count() / filter.PageSize);

            var pageItems = items.ToList();

            return new PageResponse<object?>
            {
                Items = pageItems,
                TotalPages = (uint)pageTotal,
                CurrentPageSize = (uint)filter.PageSize,
                CurrentPageNumber = (uint)filter.PageNumber,
            };
        }
    }
}
