using AutoMapper;
using HardwareHero.Services.Shared.Infrastructure;
using HardwareHero.Services.Shared.Responses;

namespace HardwareHero.Services.Shared.Repositories.Contracts
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
