namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class Cooler : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new Cooler()
            {
                Id = new Guid("07d0e938-ae97-4dcc-abe6-e119a04bd6a1"),
                ComponentNames = new[] { "Cooler", "Cooling", "Fan", "Water cooler" },
                Image = "configurator-cooler.svg",
            };
        }
    }
}
