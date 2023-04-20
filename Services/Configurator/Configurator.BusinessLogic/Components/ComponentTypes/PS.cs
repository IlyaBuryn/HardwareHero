namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class PS : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new PS()
            {
                Id = new Guid("23774b9b-9c96-421a-b336-02d687e2396b"),
                ComponentNames = new[] { "PS", "Power supply", "Power case" },
                Image = "configurator-psu.svg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "ATX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "TFX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "SFX" }
                    },
                    new ComponentTypeSpecification()
                    {
                        FormFactor = new[] { "SFX-L" }
                    }
                }
            };
        }
    }
}
