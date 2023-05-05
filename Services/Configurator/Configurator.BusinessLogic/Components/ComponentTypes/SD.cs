using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class SD : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new SD()
            {
                Id = new Guid("7153cab9-2651-4d76-a8d7-c7b94b8b594e"),
                ComponentNames = new[] { "SD", "SSD", "M2", "HDD", "Storage device" },
                Image = "configurator-sd.svg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        Types = new[] { "HDD" }
                    },
                    new ComponentTypeSpecification()
                    {
                        Types = new[] { "SSD", "M2" }
                    }
                }
            };
        }
    }
}
