using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Answers;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface IQueryRepositoryAsync<T> : IBaseRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<DataAnswer<IQueryable<T>>> FindAsync(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties);
        Task<PagedDataAnswer<T>> FindPagedAsync(Func<T, bool>? predicate, IPaginable filter,
            params Expression<Func<T, object>>[] includeProperties);
    }
}
