namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class Case : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new Case()
            {
                Id = new Guid("49f8efff-deaf-4120-9431-5b220c6c8bb9"),
                Description = "The case of the system unit serves as a place for installing the PC power supply, its working modules and the power button. However, its main properties are expressed by the noise level that affects the working environment, and the efficiency of the cooling system, on which the correct operation of the remaining PC components depends.",
                ComponentNames = new[] { "Case", "Tower" },
                Image = "case-image.jpg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "ATX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "mATX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "Mini-ATX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "E-ATX" }
                    }
                }
            };
        }
    }
}
