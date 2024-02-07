namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentAttributesFilter : FilterRequestDomain<ComponentAttributes>
    {
        public ComponentAttributesFilter()
            : base()
        {
            GroupByRequestInfo = new()
            {
                PropertyName = nameof(ComponentAttributes.AttributeName)
            };

            AddExpression(x => x.Component != null && x.Component.ComponentType != null ? x.Component.ComponentType.Name == Type || x.Component.ComponentType.FullName == Type : true);
        }

        public string? Type { get; set; } = string.Empty;

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
