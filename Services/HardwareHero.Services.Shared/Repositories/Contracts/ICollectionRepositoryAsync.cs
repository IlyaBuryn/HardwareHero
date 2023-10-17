using AutoMapper;
using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Responses;

namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface ICollectionRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T?>> GetPageAsync(int pageNumber, int pageSize);
        Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?> set, PaginationInfo paginationInfo);
        int GetTotalPageCount(IQueryable<T?> set, PaginationInfo paginationInfo);
        Task<PageResponse<MapType?>> GetMappedPageAsync<MapType>(
            IQueryable<T?> set, PaginationInfo paginationInfo, IMapper mapper);

        IQueryable<T?> GetGroupedBySet(IQueryable<T?> set, Func<T, object> groupBy);
    }
}
