namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class PS : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new PS()
            {
                Id = new Guid("23774b9b-9c96-421a-b336-02d687e2396b"),
                Description = "To some extent, the power supply performs the functions of stabilizing and protecting against minor interference of the supply voltage. The power delivered to the PSU load depends on the power of the computer system and varies from 300 (office platforms of small form factors) to a couple of thousand watts (the most high-performance workstations, servers or powerful gaming machines).",
                ComponentNames = new[] { "PS", "PSU", "Power supply", "Power case" },
                Image = "psu-image.jpg",
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
