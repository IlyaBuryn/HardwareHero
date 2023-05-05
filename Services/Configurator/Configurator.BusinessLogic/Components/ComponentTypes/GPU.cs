using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class GPU : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new GPU
            {
                Id = new Guid("d8facf00-4c48-4a31-9e03-cfb0e8d8e3fc"),
                ComponentNames = new[] { "Graphics card", "GPU" },
                Image = "configurator-gpu.svg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "NVidia",
                        Series = new[]
                        {
                            "GeForce GT", "GeForce GTX", "GeForce GTS",
                            "Titan", "GeForce RTX", "Quadro"
                        }
                    },
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "AMD",
                        Series = new[]
                        {
                            "Radeon RX"
                        }
                    },
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "Intel",
                        Series = new[]
                        {
                            "Arc"
                        }
                    }
                }
            };
        }
    }
}
