using HardwareHero.Shared.Repositories.Answers;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface ISpecificRepositoryAsync<T> : IBaseRepositoryAsync<T>
        where T : BaseEntity
    {
        Task<DataAnswer<T>> UpdateEntityValueAsync<S>(
            Expression<Func<T, bool>> predicate,
            Action<T> updateAction) where S : struct;
    }
}