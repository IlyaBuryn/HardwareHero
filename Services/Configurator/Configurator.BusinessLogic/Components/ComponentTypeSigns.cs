using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components
{
    public class ComponentTypeSigns
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string[] ComponentNames { get; set; }
        public ComponentTypeSpecification[] Specifications { get; set; }
        public string Image { get; set; }

        public virtual ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new ComponentTypeSigns();
        }
    }
}
