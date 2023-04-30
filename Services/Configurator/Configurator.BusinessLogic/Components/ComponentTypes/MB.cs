namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class MB : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new MB()
            {
                Id = new Guid("248541fb-9cc9-45cd-b11d-33732e6b48e7"),
                ComponentNames = new[] { "Motherboard", "MB", },
                Image = "configurator-motherboard.svg",
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
