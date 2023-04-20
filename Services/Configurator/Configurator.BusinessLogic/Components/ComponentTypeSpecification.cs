using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components
{
    public class ComponentTypeSpecification
    {
        public string? Manufacturer { get; set; }
        public string[]? Series { get; set; }
        public string? CPUManufacturer { get; set; }
        public string[]? FormFactor { get; set; }
        public string[]? Types { get; set; }
    }
}