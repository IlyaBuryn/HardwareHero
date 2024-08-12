using HardwareHero.Filter.Operations;
using System.Linq.Expressions;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentsFilter : FilterRequestDomain<Component>, 
        ISelectable<Component>, IFilterable<Component>, ISortable<Component>, IPaginable
    {
        public ComponentsFilter()
        {
            SetupFilterExpressions();
            SetupSortByExpressions();
        }

        public string? SearchString { get; set; }
        public string? Type { get; set; }
        public string? SortByProperty { get; init; }
        public bool SortByDescending { get; init; } = true;
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        public object SetupSelectFields(Component? item)
        {
            return new Component
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ComponentType = item.ComponentType,
            };
        }

        public void SetupFilterExpressions()
        {
            FilterExpressions[nameof(Component)] =
                component =>
                (string.IsNullOrEmpty(SearchString) || (component.Name.Contains(SearchString) || component.Description.Contains(SearchString))) &&
                (string.IsNullOrEmpty(Type) || (component.ComponentType != null ? component.ComponentType.Name == Type || component.ComponentType.FullName == Type : true));
        }

        public void SetupSortByExpressions()
        {
            SortByExpressions[nameof(Component.Id)] = component => component.Id;
            SortByExpressions[nameof(Component.Name)] = component => component.Name;
            SortByExpressions[nameof(Component.ComponentType)] = component => component.ComponentType.Name;
        }

        public Expression<Func<Component, bool>>? OnGetFilterExpression()
            => GetFilterExpression(nameof(Component));

        public Expression<Func<Component, object>>? OnGetSortExpression(string? sortByProperty)
            => GetSortExpression(sortByProperty);
    }
}
