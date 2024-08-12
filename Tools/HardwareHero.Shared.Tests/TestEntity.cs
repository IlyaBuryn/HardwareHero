using HardwareHero.Shared.Models;

namespace HardwareHero.Shared.Tests
{
    public class TestEntity : BaseEntity
    {
        public string? StringValue { get; set; }
        public virtual ICollection<RelatedTestEntity> RelatedTestEntities { get; set; } 
            = new List<RelatedTestEntity>(3);

        public virtual AnotherTestEntity AnotherTestEntity { get; set; }
    }

    public class RelatedTestEntity : BaseEntity
    {
        public int TestIntValue { get; set; }
        public Guid TestEntityId { get; set; }
    }

    public class AnotherTestEntity : BaseEntity
    {
        public double TestDoubleValue { get; set; }
        public Guid TestEntityId { get; set; }
        public virtual DeepRelatedTestEntity? DeepRelatedTestEntity { get; set; }
    }

    public class DeepRelatedTestEntity : BaseEntity
    {
        public bool TestBoolValue { get; set; }
        public Guid AnotherTestEntityId { get; set; }
    }
}
