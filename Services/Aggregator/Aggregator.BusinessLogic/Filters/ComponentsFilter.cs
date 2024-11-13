using Aggregator.DataAccess.Models.Components;
using HardwareHero.Filter.Operations;
using System.Linq.Expressions;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentsFilter : IPaginable
    {
        public string? SearchString { get; set; }
        public string? TypeId { get; set; }
        public bool? SortByViews { get; set; } = null;
        public bool? SortByRelevance { get; set; } = null;
        public bool? SortByRating { get; set; } = null;
        public List<ComponentAttribute>? Attributes { get; set; } = null;

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }


        //public void SetupFilterExpressions()
        //{
        //    FilterExpressions[nameof(Component)] =
        //        component =>
        //        (string.IsNullOrEmpty(SearchString) || (component.Name.Contains(SearchString) || component.Description.Contains(SearchString))) &&
        //        (string.IsNullOrEmpty(Type) || (component.ComponentType != null ? component.ComponentType.Name == Type || component.ComponentType.FullName == Type : true));
        //}

        //public void SetupSortByExpressions()
        //{
        //    SortByExpressions[nameof(Component.Id)] = component => component.Id;
        //    SortByExpressions[nameof(Component.Name)] = component => component.Name;
        //    SortByExpressions[nameof(Component.ComponentType)] = component => component.ComponentType.Name;
        //}

        //public Expression<Func<Component, bool>>? OnGetFilterExpression()
        //    => GetFilterExpression(nameof(Component));

        //public Expression<Func<Component, object>>? OnGetSortExpression(string? sortByProperty)
        //    => GetSortExpression(sortByProperty);
    }
}
