using HardwareHero.Filter.Operations;
using System.Linq.Expressions;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentAttributesFilter : FilterRequestDomain<ComponentAttributes>,
        IFilterable<ComponentAttributes>, IGroupable<ComponentAttributes>, IPaginable, ISortable<ComponentAttributes>
    {
        public ComponentAttributesFilter()
        {
            SetupFilterExpressions();
            SetupGroupByExpressions();
            SetupSortByExpressions();
        }

        public string? Type { get; set; }
        public string? GroupByProperty { get; init; }
        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
        public string? SortByProperty { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
        public bool SortByDescending { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

        public void SetupFilterExpressions()
        {
            FilterExpressions[nameof(ComponentAttributes)] =
                attr => (attr.Component == null || attr.Component.ComponentType == null) || (attr.Component.ComponentType.Name == Type || attr.Component.ComponentType.FullName == Type);
        }

        public void SetupGroupByExpressions()
        {
            GroupByExpressions[nameof(ComponentAttributes.AttributeName)] =
                attr => attr.AttributeName;
        }

        public void SetupSortByExpressions()
        {
            SortByExpressions[nameof(ComponentAttributes.AttributeName)] = attr => attr.AttributeName;
            SortByExpressions[nameof(ComponentAttributes.AttributeValue)] = attr => attr.AttributeValue;
        }

        public Expression<Func<ComponentAttributes, bool>>? OnGetFilterExpression()
            => GetFilterExpression(nameof(ComponentAttributes));

        public Expression<Func<ComponentAttributes, object>>? OnGetGroupExpression(string groupByProperty)
            => GetGroupExpression(groupByProperty);

        public Expression<Func<ComponentAttributes, object>>? OnGetSortExpression(string? sortByProperty)
            => GetSortExpression(sortByProperty);
    }
}
