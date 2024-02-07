namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class SD : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new SD()
            {
                Id = new Guid("7153cab9-2651-4d76-a8d7-c7b94b8b594e"),
                Description = "An SSD is a solid-state, non-mechanical storage device. The speed of an SSD drive is much higher than that of a hard drive, so it is advisable to use it for operating system files by installing other programs on the HDD.\nA hard disk (\"hard drive\", HDD) is a device that stores operating system files. The speed of rotation of the \"hard drive\" disks directly affects the user's comfort from working with the OS. Low noise level, high fault tolerance and optimal price/volume ratio have defined the HDD as a device for storing important information.",
                ComponentNames = new[] { "SD", "SSD", "M2", "HDD", "Storage device" },
                Image = "ssd-image.jpg",
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
