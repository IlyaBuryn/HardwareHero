using HardwareHero.Filter.Operations;

namespace HardwareHero.Shared.Extensions.Filter
{
    public interface IPaginableFilter<T> : IPaginable
        where T : BaseEntity
    {
        Func<T, bool> BuildFilterPredicate();
        IQueryable<T> ApplySorting(IQueryable<T> query);
    }
}
