using AutoMapper;
using HardwareHero.Services.Shared.Models;
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

        public async Task<IEnumerable<T?>> GetPageAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            var result = _dbSet.Skip(skip).Take(pageSize);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?> set, PaginationInfo paginationInfo)
        {
            int skip = (paginationInfo.PageNumber - 1) * paginationInfo.PageSize;
            IQueryable<T?> result = set.Skip(skip).Take(paginationInfo.PageSize);

            return await result.ToListAsync();
        }

        public async Task<PageResponse<MapType?>> GetMappedPageAsync<MapType>(IQueryable<T?> set, PaginationInfo paginationInfo, IMapper mapper)
        {
            var items = await GetPageAsync(set, paginationInfo);
            var pageTotal = GetTotalPageCount(set, paginationInfo);

            var pageItems = new List<MapType?>();
            if (mapper != null)
            {
                pageItems = mapper.Map<List<MapType?>>(items);
            }

            return new PageResponse<MapType?>
            {
                Items = pageItems,
                TotalPages = pageTotal,
            };
        }

        public int GetTotalPageCount(IQueryable<T?> set, PaginationInfo paginationInfo)
        {
            return (int)Math.Ceiling((double)set.Count() / paginationInfo.PageSize);
        }

        public IQueryable<T?> GetGroupedBySet(IQueryable<T?> set, Func<T, object> groupBy)
        {
            var groupedData = set.GroupBy(groupBy);
            return groupedData.SelectMany(x => x).AsQueryable();
        }
    }
}
