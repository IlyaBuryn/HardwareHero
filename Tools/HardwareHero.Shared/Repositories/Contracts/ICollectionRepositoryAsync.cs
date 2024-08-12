using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Responses;
using System.Diagnostics.CodeAnalysis;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface ICollectionRepositoryAsync<T> : ICrudRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<IEnumerable<T?>> GetPageAsync(IQueryable<T?>? query, [NotNull] IPaginable filter);
        Task<int> GetTotalPageCountAsync(IQueryable<T?>? query, [NotNull] IPaginable filter);
        Task<PageResponse<T?>> GetMappedPageAsync(IQueryable<T?>? query, [NotNull] IPaginable filter);
        Task<PageResponse<object?>> GetObjectPageAsync(IQueryable<object?>? query, [NotNull] IPaginable filter);
    }
}
