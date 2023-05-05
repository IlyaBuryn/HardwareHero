using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class Case : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new Case()
            {
                Id = new Guid("49f8efff-deaf-4120-9431-5b220c6c8bb9"),
                ComponentNames = new[] { "Case", "Tower" },
                Image = "configurator-case.svg",
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
