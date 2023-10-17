using HardwareHero.Services.Shared.Models.Aggregator;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.DTOs.Aggregator
{
    public class ComponentImagesDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public string Image { get; set; }
        public ComponentDto? Component { get; set; }

        public byte[] ImageData { get; set; }

        public static Expression<Func<ComponentImages, string>>[] GetFileNameExpression() =>
            new Expression<Func<ComponentImages, string>>[]
                {
                    x => x.ComponentId.ToString(),
                    x => x.Image
                };
    }
}
