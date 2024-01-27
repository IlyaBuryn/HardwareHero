using AutoMapper;
using HardwareHero.Services.Shared.Infrastructure;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Services.Shared.Repositories.EF
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

        public async Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?>? query, PaginationInfo paginationInfo)
        {
            if (query == null)
            {
                query = Enumerable.Empty<T?>().AsQueryable();
            }

            int skip = (paginationInfo.PageNumber - 1) * paginationInfo.PageSize;
            IQueryable<T?> result = query.Skip(skip).Take(paginationInfo.PageSize);

            return await result.ToListAsync();
        }

        public async Task<PageResponse<MapType?>> GetMappedPageAsync<MapType>(IQueryable<T?>? query, PaginationInfo paginationInfo, IMapper mapper)
        {
            if (query == null)
            {
                query = Enumerable.Empty<T?>().AsQueryable();
            }

            var items = await GetPageAsync(query, paginationInfo);
            var pageTotal = GetTotalPageCount(query, paginationInfo);

            var pageItems = new List<MapType?>();
            if (mapper != null)
            {
                pageItems = mapper.Map<List<MapType?>>(items);
            }

            return new PageResponse<MapType?>
            {
                Items = pageItems,
                TotalPages = pageTotal,
                CurrentPaginationInfo = paginationInfo,
            };
        }

        public int GetTotalPageCount(IQueryable<T?>? query, PaginationInfo paginationInfo)
        {
            if (query == null)
            {
                query = Enumerable.Empty<T?>().AsQueryable();
            }

            var result = (int)Math.Ceiling((double)query.Count() / paginationInfo.PageSize);
            
            return result;
        }
    }
}
