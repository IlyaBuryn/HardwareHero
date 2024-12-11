using Contributor.Domain.DTO.Chat;

namespace Contributor.Domain.DTO.Contributors
{
    public class ContributorModelDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ContributorUserAccountId { get; set; }
        public Guid ContributorDetailsId { get; set; }

        public ContributorUserAccountDto ContributorUserAccount { get; set; }
            = new();
        public ContributorDetailsDto ContributorDetails { get; set; }
            = new();

        public ICollection<ChatRoomDto>? ChatRooms { get; set; } 
            = new List<ChatRoomDto>();
    }
}