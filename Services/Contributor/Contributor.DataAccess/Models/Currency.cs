using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class Currency : BaseEntity
    {
        public string Code { get; set; }
        public char? Symbol { get; set; }
    }
}
