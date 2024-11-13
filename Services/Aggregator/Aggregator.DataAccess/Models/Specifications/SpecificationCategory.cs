using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Specifications
{
    public class SpecificationCategory : BaseEntity
    {
        public string Name { get; set; }
        public int Priority { get; set; }
    }
}
