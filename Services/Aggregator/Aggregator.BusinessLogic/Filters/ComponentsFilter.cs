namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentsFilter : FilterRequestDomain<Component>
    {
        public ComponentsFilter()
            : base()
        {
            AddTransformationPattern(SelectionPattern);
        }

        public override void SetFilterExpression()
        {
            base.SetFilterExpression();

            if (SearchString != null)
                AddFilterCondition(x => x.Name.Contains(SearchString) || x.Description.Contains(SearchString));

            if (Type != null)
                AddFilterCondition(x => x.ComponentType != null ? x.ComponentType.Name == Type || x.ComponentType.FullName == Type : true);

            if (AttributeFilters != null && AttributeFilters.Count() != 0)
            {
                foreach (var attributeFilter in AttributeFilters)
                {
                    AddFilterCondition(x => x.ComponentAttributes
                        .Any(a => a.AttributeName == attributeFilter.Key &&
                                  a.AttributeValue.Contains(attributeFilter.Value)));
                }
            }

            
        }

        public string? SearchString { get; set; }
        public string? Type { get; set; }
        public Dictionary<string, string>? AttributeFilters { get; set; }

        

        public static Component? SelectionPattern(Component? refItem)
        {
            //if (refItem.ComponentImages != null)
            //{
            //    refItem.ComponentImages = refItem.ComponentImages.Select(ci => new ComponentImages
            //    {
            //        Id = ci.Id,
            //        Image = ci.Image,
            //        Component = null
            //    }).ToList();
            //}

            refItem.ComponentImages = null;
            refItem.ComponentAttributes = null;

            return refItem;

            //return new Component
            //{
            //    Id = refItem.Id,
            //    Name = refItem.Name,
            //    Description = refItem.Description,
            //    ComponentType = refItem.ComponentType,

            //    ComponentAttributes = null,
            //    ComponentTypeId = Guid.Empty,
            //    ComponentImages = null,
            //};
        }
    }
}
