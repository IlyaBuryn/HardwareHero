namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class MB : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new MB()
            {
                Id = new Guid("248541fb-9cc9-45cd-b11d-33732e6b48e7"),
                Description = "The motherboard is the main PC board that connects all the components of the system unit into one logical whole. Its open architecture allows you to assemble the system yourself. Any module, be it a processor, RAM or a video card, has a connector corresponding only to it, where the necessary power supply is supplied.",
                ComponentNames = new[] { "Motherboard", "MB", },
                Image = "mb-image.jpg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        CPUManufacturer = "Intel"
                    },
                    new ComponentTypeSpecification()
                    {
                        CPUManufacturer = "AMD"
                    },
                }
            };
        }
    }
}
