using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }
        public string? Symbol { get; set; }
    }
}
