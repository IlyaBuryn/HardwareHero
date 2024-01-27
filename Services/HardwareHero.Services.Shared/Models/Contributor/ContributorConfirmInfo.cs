using HardwareHero.Services.Shared.Infrastructure;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    public class ContributorConfirmInfo : BaseEntity
    {
        public bool IsConfirmed { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
