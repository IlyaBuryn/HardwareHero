using Aggregator.DataAccess.Models.Components;
using HardwareHero.Shared.Extensions.Filter;

namespace Aggregator.BusinessLogic.Filters
{
    public enum SortByType
    {
        None = 0,
        Views,
        Relevance,
        Rating,
        Price
    }

    public class ComponentsFilter : IPaginableFilter<Component>
    {
        public string? SearchString { get; set; }
        public Guid? TypeId { get; set; }
        public SortByType SortByType { get; set; } = SortByType.None;
        public bool SortByDescending { get; set; } = true;

        // TODO: skip for now...
        public List<ComponentAttribute>? Attributes { get; set; } = null;

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        public Func<Component, bool> BuildFilterPredicate()
        {
            return x =>
            {
                bool matches = true;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    matches &= x.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase);
                }

                if (TypeId.HasValue)
                {
                    matches &= x.ComponentTypeId == TypeId.Value;
                }

                if (Attributes != null && Attributes.Any())
                {
                    //matches &= Attributes.All(attr =>
                    //    x.Attributes.Any(xAttr =>
                    //        xAttr.Key == attr.Key && xAttr.Value == attr.Value));
                }

                return matches;
            };
        }

        public IQueryable<Component> ApplySorting(IQueryable<Component> query)
        { 
            query = _sortStrategies[SortByType].Invoke(query, SortByDescending);

            return query;
        }

        private readonly Dictionary<SortByType, Func<IQueryable<Component>, bool, IQueryable<Component>>> _sortStrategies = new()
        {
            { SortByType.None, (query, descending) => query },

            //{ SortByType.Price, (query, descending) => descending
            //    ? query.OrderByDescending(x => x.Price)
            //    : query.OrderBy(x => x.Price) },

            { SortByType.Views, (query, descending) => descending
                ? query.OrderByDescending(x => x.ComponentMetric.ViewsCount)
                : query.OrderBy(x => x.ComponentMetric.ViewsCount) },

            { SortByType.Relevance, (query, descending) => descending
                ? query.OrderByDescending(x => x.ComponentMetric.ReviewCount)
                : query.OrderBy(x => x.ComponentMetric.ReviewCount) },

            { SortByType.Rating, (query, descending) => descending
                ? query.OrderByDescending(x => x.ComponentMetric.Rating)
                : query.OrderBy(x => x.ComponentMetric.Rating) },
        };
    }
}
