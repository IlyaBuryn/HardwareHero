namespace Contributor.DTOs.Domain.Contributors
{
    public class ContributorConfirmInfoDto
    {
        public Guid Id { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
