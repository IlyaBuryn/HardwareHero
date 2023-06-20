using MongoDB.Bson.Serialization.Attributes;

namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class Cooler : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new Cooler()
            {
                Id = new Guid("07d0e938-ae97-4dcc-abe6-e119a04bd6a1"),
                Description = "A computer cooling system is a set of tools for removing heat from computer components that heat up during operation. To increase the passing air flow, fans are additionally used (the combination of it and the radiator is called a cooler). Coolers are mainly installed on the central and graphic processors.",
                ComponentNames = new[] { "Cooler", "Cooling", "Fan", "Water cooler" },
                Image = "cooler-image.jpg",
            };
        }
    }
}
