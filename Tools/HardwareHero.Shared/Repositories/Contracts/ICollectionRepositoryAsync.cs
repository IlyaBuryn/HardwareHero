using AutoMapper;
using HardwareHero.Shared.Responses;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface ICollectionRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?>? query, PaginationInfo paginationInfo);
        int GetTotalPageCount(IQueryable<T?>? query, PaginationInfo paginationInfo);
        Task<PageResponse<MapType?>> GetMappedPageAsync<MapType>(
            IQueryable<T?>? query, PaginationInfo paginationInfo, IMapper mapper);
    }
}
