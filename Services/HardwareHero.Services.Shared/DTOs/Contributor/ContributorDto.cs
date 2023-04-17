using HardwareHero.Services.Shared.Models.Contributor;

namespace HardwareHero.Services.Shared.DTOs.Contributor
{
    public class ContributorDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Region { get; set; }
        public SubscriptionInfoDto SubscriptionInfo { get; set; }
        public Guid SubscriptionInfoId { get; set; }
        public ReferenceDto? ComponentRef { get; set; }
        public Guid? ComponentRefId { get; set; }
        public ReferenceDto? ReviewRef { get; set; }
        public Guid? ReviewRefId { get; set; }
        public ContributorExcellenceDto ContributorExcellence { get; set; }
        public Guid ContributorExcellenceId { get; set; }
        public ICollection<ChatRoom> ChatRooms { get; set; }
    }
}
