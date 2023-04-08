using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareHero.Services.Shared.Models.Contributor
{
    [Table("Contributors")]
    public class ContributorModel : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Region { get; set; }
        public virtual SubscriptionInfo SubscriptionInfo { get; set; }
        public Guid SubscriptionInfoId { get; set; }
        public virtual Reference? ComponentRef { get; set; }
        public Guid? ComponentRefId { get; set; }
        public virtual Reference? ReviewRef { get; set; }
        public Guid? ReviewRefId { get; set; }
        public virtual ContributorExcellence ContributorExcellence { get; set; }
        public Guid ContributorExcellenceId { get; set; }
    }
}
