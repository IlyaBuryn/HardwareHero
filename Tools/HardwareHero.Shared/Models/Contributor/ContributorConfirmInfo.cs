namespace HardwareHero.Shared.Models.Contributor
{
    public class ContributorConfirmInfo : BaseEntity
    {
        public bool IsConfirmed { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
