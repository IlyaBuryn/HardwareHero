using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
