using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentAttributesFilter : Filter<ComponentAttributes>
    {
        [FilterProperty("Component.ComponentType.Name", FilterOperation.Equal)]
        [OrFilterProperty("Component.ComponentType.FullName", FilterOperation.Equal)]
        public string? Type { get; set; }
        public override string? GroupBy { get; set; } = "AttributeName";

        public override IQueryable<ComponentAttributes?>? GroupedPattern(IQueryable<IGrouping<object, ComponentAttributes?>> groups)
        {
            var query = groups.Select(group => new ComponentAttributes
            {
                AttributeName = (string)(group.Key is string ? group.Key : string.Empty),
                AttributeValue = string.Join("|", group.Select(attr => attr.AttributeValue).Distinct().ToList()),
            });

            return query;
        }
    }
}
