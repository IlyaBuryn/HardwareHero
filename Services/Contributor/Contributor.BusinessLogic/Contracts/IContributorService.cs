using Contributor.DTOs.Domain.Contributors;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorService
    {
        Task<Guid?> SignUpContributorAsync(ContributorModelDto contributorToAdd);
        Task<bool> RemoveContributorAsync(Guid contributorId);
        Task<ContributorModelDto?> GetContributorByNameAsync(string name);
        Task<ContributorModelDto?> GetContributorByUserIdAsync(Guid userId);
        Task<PageResponse<ContributorModelDto?>> GetContributorsAsPageAsync(ContributorsFilter filter);

        Task<ContributorConfirmInfoDto?> GetConfirmInfoByContributorIdAsync(Guid contributorId);
        Task<bool> ChangeContributorConfirmInfoAsync(Guid contributorId, ContributorConfirmInfoDto info);
    }
}
