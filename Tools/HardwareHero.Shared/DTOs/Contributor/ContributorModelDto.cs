namespace HardwareHero.Shared.DTOs.Contributor
{
    public class ContributorModelDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ContributorConfirmInfoId { get; set; }
        public Guid ContributorExcellenceId { get; set; }
        public Guid? SubscriptionPlanInfoId { get; set; }

        public ContributorConfirmInfoDto? ContributorConfirmInfo { get; set; }
        public SubscriptionPlanInfoDto? SubscriptionPlanInfo { get; set; }
        public ContributorExcellenceDto ContributorExcellence { get; set; }

        public ICollection<ChatRoomDto>? ChatRooms { get; set; } = new List<ChatRoomDto>();
    }
}
