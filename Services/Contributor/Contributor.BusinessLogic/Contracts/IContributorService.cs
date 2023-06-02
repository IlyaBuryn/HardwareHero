using HardwareHero.Services.Shared.DTOs.Contributor;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorService
    {
        Task<Guid?> AddContributorAsync(ContributorDto contributorToAdd);
        Task<bool> UpdateContributorAsync(ContributorDto contributorToUpdate);
        Task<bool> RemoveContributorAsync(Guid contributorId);
        Task<ContributorDto?> GetContributorByNameAsync(string name);
        Task<ContributorDto?> GetContributorByUserId(Guid userId);
        Task<List<ContributorDto?>> GetContributorsAsync();
        Task<ReferenceDto?> GetReviewReferencesByContributorIdAsync(Guid contributorId);
        Task<ReferenceDto?> GetComponentReferencesByContributorIdAsync(Guid contributorId);
        Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId);
    }
}
