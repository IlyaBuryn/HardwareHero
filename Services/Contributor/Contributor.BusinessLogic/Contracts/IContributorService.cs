namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorService
    {
        Task<Guid?> AddContributorAsync(ContributorDto contributorToAdd);
        Task<bool> UpdateContributorAsync(ContributorDto contributorToUpdate);
        Task<bool> RemoveContributorAsync(Guid contributorId);
        Task<List<ContributorDto>> GetContributorsAsync();
        Task<ReferenceDto> GetReviewReferencesByContributorIdAsync(Guid contributorId);
        Task<ReferenceDto> GetComponentReferencesByContributorIdAsync(Guid contributorId);
        // TODO: method to get ApplicationUser by contributor
    }
}
