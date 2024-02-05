using HardwareHero.Filter.RequestsModels;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentsFilter : FilterRequestDomain<Component>
    {
        public ComponentsFilter()
            : base()
        {
            AddExpression(x => x.Name.Contains(SearchString) || x.Description.Contains(SearchString));
            AddExpression(x => x.ComponentType != null ? x.ComponentType.Name == Type || x.ComponentType.FullName == Type : true);
        }

        public string? SearchString { get; set; } = string.Empty;
        public string? Type { get; set; } = string.Empty;
        public Dictionary<string, string>? AttributeFilters { get; set; }


        public override Component SelectionPattern(Component refItem)
        {
            if (refItem.ComponentImages != null)
            {
                refItem.ComponentImages = refItem.ComponentImages.Select(ci => new ComponentImages
                {
                    Id = ci.Id,
                    Image = ci.Image,
                    Component = null
                }).ToList();
            }

            return new Component
            {
                Id = refItem.Id,
                Name = refItem.Name,
                Description = refItem.Description,
                ComponentTypeId = refItem.ComponentTypeId,
                ComponentImages = refItem.ComponentImages,
            };
        }
    }
}
