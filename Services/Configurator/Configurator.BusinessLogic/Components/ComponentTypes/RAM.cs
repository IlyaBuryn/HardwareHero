namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class RAM : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new RAM()
            {
                Id = new Guid("573c50b6-6dc1-4c3b-889d-a2965577a8b1"),
                ComponentNames = new[] { "RAM", "Random access memory" },
                Image = "configurator-ram.svg",
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
