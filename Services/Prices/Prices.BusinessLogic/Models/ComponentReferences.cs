using HardwareHero.Shared.Models;

namespace Prices.BusinessLogic.Models
{
    public class ComponentReferences : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public string Link { get; set; }
    }
}
