using Aggregator.DataAccess.Models.Components;
using HardwareHero.Filter.Responses;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class GroupItemExtensions
    {
        //public static IQueryable<ComponentAttribute> SeparateGroupsToComponentAttributes(this IEnumerable<GroupItem<ComponentAttribute>> groups)
        //{
        //    var query = groups.Select(group => new ComponentAttribute
        //    {
        //        AttributeName = (string)(group.Key is string ? group.Key : string.Empty),
        //        AttributeValue = string.Join("<value-separator>", group.Query.Select(attr => attr.AttributeValue).Distinct().ToList()),
        //    }).AsQueryable();

        //    return query;
        //}

        //public static IQueryable<ComponentAttribute> GroupByAttributeName(this IQueryable<ComponentAttribute> attributes)
        //{
        //    var groupedAttributes = attributes
        //    .GroupBy(c => c.AttributeName)
        //    .Select(g => new ComponentAttribute
        //    {
        //        AttributeName = g.Key,
        //        AttributeValue = string.Join(" | ", g.Select(c => c.AttributeValue))
        //    });

        //    return groupedAttributes.AsQueryable();
        //}
    }
}
