using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class CPU : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new CPU()
            {
                Id = new Guid("ed0ce5e4-7b63-4e94-a579-070a2e5ec6de"),
                Description = "The processor (CPU, CPU) is the \"brain\" of the computer. It consists of several million transistors grouped into cores. Each CPU core is capable of processing a separate task, so their number and clock speed directly affect the overall speed of operations.",
                ComponentNames = new[] { "Processor", "CPU" },
                Image = "cpu-image.jpg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        CPUManufacturer = "Intel",
                        Series = new[]
                        {
                            "Celeron", "Pentium", "Core",
                            "Core I3", "Core I5", "Core I7", "Core I9",
                            "Xeon"
                        }
                    },
                    new ComponentTypeSpecification()
                    {
                        CPUManufacturer = "AMD",
                        Series = new[]
                        {
                            "A6", "A8", "Athlon", "Phenom", "FX",
                            "Ryzen 3", "Ryzen 5", "Ryzen 7", "Ryzen 9",
                            "Threadripper"
                        }
                    }
                }
            };
        }
    }
}
