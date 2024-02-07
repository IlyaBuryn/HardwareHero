namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class RAM : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new RAM()
            {
                Id = new Guid("573c50b6-6dc1-4c3b-889d-a2965577a8b1"),
                Description = "Random access memory (RAM) performs the function of a temporary store of data that is needed for the processor to work at a particular point in time. The key indicators of \"RAM\" are expressed by the speed of receiving / transmitting information and the amount of stored data. The higher they are, the more efficient the CPU is.",
                ComponentNames = new[] { "RAM", "Random access memory" },
                Image = "ram-image.jpg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        Types = new[]
                        {
                            "DDR3", "DDR4", "DDR5"
                        }
                    }
                }
            };
        }
    }
}
