using HardwareHero.Shared.Models;

namespace Contributor.DataAccess.Models
{
    public class ContributorConfirmInfo : BaseEntity
    {
        public bool IsConfirmed { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
