using Contributor.BusinessLogic.Filters;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Responses;

namespace Contributor.BusinessLogic.Contracts
{
    public interface IContributorService
    {
        Task<Guid?> SignUpContributorAsync(ContributorModelDto contributorToAdd);
        Task<bool> RemoveContributorAsync(Guid contributorId);
        Task<ContributorModelDto?> GetContributorByExcNameAsync(string name);
        Task<ContributorModelDto?> GetContributorByUserIdAsync(Guid userId);
        Task<PageResponse<ContributorModelDto?>> GetContributorsAsPageAsync(ContributorsFilter filter);

        Task<ContributorConfirmInfoDto?> GetConfirmInfoByContributorIdAsync(Guid contributorId);
        Task<bool> ChangeConfirmInfoForContributorAsync(Guid contributorId, ContributorConfirmInfoDto info);
    }
}
