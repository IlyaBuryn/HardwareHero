namespace Configurator.BusinessLogic.Components.ComponentTypes
{
    public class GPU : ComponentTypeSigns
    {
        public override ComponentTypeSigns ConfigureSpecificDescription()
        {
            return new GPU
            {
                Id = new Guid("d8facf00-4c48-4a31-9e03-cfb0e8d8e3fc"),
                Description = "A video card (graphic adapter) displays an image on a computer monitor. A powerful graphics processor, a large amount of memory and an active cooling system are a guarantee of enjoying the quality of the “picture” while playing games or when watching&nbsp;video content&nbsp;on the net.",
                ComponentNames = new[] { "Graphics card", "GPU" },
                Image = "gpu-image.jpg",
                Specifications = new[]
                {
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "NVidia",
                        Series = new[]
                        {
                            "GeForce GT", "GeForce GTX", "GeForce GTS",
                            "Titan", "GeForce RTX", "Quadro"
                        }
                    },
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "AMD",
                        Series = new[]
                        {
                            "Radeon RX"
                        }
                    },
                    new ComponentTypeSpecification()
                    {
                        Manufacturer = "Intel",
                        Series = new[]
                        {
                            "Arc"
                        }
                    }
                }
            };
        }
    }
}
