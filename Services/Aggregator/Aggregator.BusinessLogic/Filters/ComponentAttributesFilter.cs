using Aggregator.DataAccess.Models.Components;
using HardwareHero.Filter.Operations;
using System.Linq.Expressions;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentAttributesFilter : IPaginable
    {
        public string? TypeId { get; set; }
        public bool SortByDescending { get; set; } = false;

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }

        //public void SetupFilterExpressions()
        //{
        //    FilterExpressions[nameof(ComponentAttribute)] =
        //        attr => string.IsNullOrEmpty(Type) || 
        //            (attr.Component == null || attr.Component.ComponentType == null ? true : attr.Component.ComponentType.Name == Type);
        //}


        //public void SetupGroupByExpressions()
        //{
        //    GroupByExpressions[nameof(ComponentAttribute.AttributeName)] =
        //        attr => attr.AttributeName;
        //}

        //public void SetupSortByExpressions()
        //{
        //    SortByExpressions[nameof(ComponentAttribute.AttributeName)] = attr => attr.AttributeName;
        //}

        //public Expression<Func<ComponentAttribute, bool>>? OnGetFilterExpression()
        //    => GetFilterExpression(nameof(ComponentAttribute));

        //public Expression<Func<ComponentAttributes, object>>? OnGetGroupExpression(string groupByProperty)
        //    => GetGroupExpression(groupByProperty);

        //public Expression<Func<ComponentAttributes, object>>? OnGetSortExpression(string? sortByProperty)
        //    => GetSortExpression(sortByProperty);
    }
}
