using Aggregator.DataAccess.Models.Components;
using HardwareHero.Shared.Models;

namespace Aggregator.DataAccess.Models.Specifications
{
    public class SpecificationAttribute : BaseEntity
    {
        public Guid ComponentTypeId { get; set; }
        public Guid? SpecificationFilterTypeId { get; set; }
        public Guid SpecificationCategoryId { get; set; }
        public string Key { get; set; }
        public string? Unit { get; set; }

        public virtual ComponentType? ComponentType { get; set; }
        public virtual SpecificationFilterType? SpecificationFilterType { get; set; }
        public virtual SpecificationCategory? SpecificationCategory { get; set; }
    }
}
