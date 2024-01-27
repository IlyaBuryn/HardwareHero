using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Aggregator;

namespace Aggregator.BusinessLogic.Filters
{
    public class ComponentsFilter : Filter<Component>
    {
        [FilterProperty("Name", FilterOperation.Contains)]
        [OrFilterProperty("Description", FilterOperation.Contains)]
        public string? SearchString { get; set; }
        [FilterProperty("ComponentType.Name", FilterOperation.Equal)]
        [OrFilterProperty("ComponentType.FullName", FilterOperation.Equal)]
        public string? Type { get; set; }

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
