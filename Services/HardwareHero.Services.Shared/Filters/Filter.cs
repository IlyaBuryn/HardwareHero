using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Filters
{
    public class Filter<T>
    {
        public virtual PaginationInfo PaginationInfo { get; set; } = new PaginationInfo(1, 1);
        public virtual string SortBy { get; set; } = "Id";
        public virtual string SortOrder { get; set; } = "asc";
        public virtual string? GroupBy { get; set; }
        public virtual T SelectionPattern(T refItem)
        {
            return refItem;
        }

        public virtual IQueryable<T?>? GroupedPattern(IQueryable<IGrouping<object, T?>> groups)
        {
            return new List<T?>().AsQueryable();
        }
    }
}
