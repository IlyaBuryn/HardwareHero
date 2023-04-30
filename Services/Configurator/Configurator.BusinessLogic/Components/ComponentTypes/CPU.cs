namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class CPU : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new CPU()
            {
                Id = new Guid("ed0ce5e4-7b63-4e94-a579-070a2e5ec6de"),
                ComponentNames = new[] { "Processor", "CPU" },
                Image = "configurator-cpu.svg",
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
