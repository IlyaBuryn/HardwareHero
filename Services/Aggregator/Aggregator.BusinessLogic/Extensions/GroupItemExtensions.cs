using HardwareHero.Filter.Responses;

namespace Aggregator.BusinessLogic.Extensions
{
    public static class GroupItemExtensions
    {
        public static IQueryable<ComponentAttributes> SeparateGroupsToComponentAttributes(this IEnumerable<GroupItem<ComponentAttributes>> groups)
        {
            var query = groups.Select(group => new ComponentAttributes
            {
                AttributeName = (string)(group.Key is string ? group.Key : string.Empty),
                AttributeValue = string.Join("<value-separator>", group.Query.Select(attr => attr.AttributeValue).Distinct().ToList()),
            }).AsQueryable();

            return query;
        }
    }
}
