using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Filters;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;
using HardwareHero.Services.Shared.Responses;
using HardwareHero.Services.Shared.Extensions;
using Contributor.BusinessLogic.Filters;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorService : IContributorService
    {
        private readonly ICollectionRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IValidationRepository<ContributorModel> _contributorValidationRepo;
        private readonly IValidationRepository<ContributorExcellence> _contributorExcValidationRepo;

        private readonly ICrudRepositoryAsync<ContributorConfirmInfo> _contributorConfirmInfoRepo;
        private readonly ICrudRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly ICrudRepositoryAsync<ContributorExcellence> _excellenceRepo;
        
        private readonly IImageRepositoryAsync _imagesRepo;

        private readonly IMapper _mapper;

        public ContributorService(
            ICollectionRepositoryAsync<ContributorModel> contributorRepo,
            IValidationRepository<ContributorModel> contributorValidationRepo,
            IValidationRepository<ContributorExcellence> contributorExcValidationRepo,
            ICrudRepositoryAsync<ContributorConfirmInfo> contributorConfirmInfoRepo,
            ICrudRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            ICrudRepositoryAsync<ContributorExcellence> excellenceRepo,
            IImageRepositoryAsync imagesRepo,
            IMapper mapper)
        {
            _contributorRepo = contributorRepo;
            _contributorValidationRepo = contributorValidationRepo;
            _contributorExcValidationRepo = contributorExcValidationRepo;
            _contributorConfirmInfoRepo = contributorConfirmInfoRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _excellenceRepo = excellenceRepo;
            _imagesRepo = imagesRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> SignUpContributorAsync(ContributorModelDto contributorToAdd)
        {
            contributorToAdd.Id = Guid.NewGuid();

            _contributorValidationRepo
                .CheckIfObjectAlreadyExist(x => x.UserId == contributorToAdd.UserId);

            _contributorExcValidationRepo
                .CheckIfObjectAlreadyExist(x => x.Name == contributorToAdd.ContributorExcellence.Name);

            await _imagesRepo.SaveImageAsync(
                contributorToAdd.ContributorExcellence.ImageData,
                contributorToAdd.ContributorExcellence.Logo, null);

            var contributor = _mapper.Map<ContributorModel>(contributorToAdd);
            var contributorResult = await _contributorRepo.CreateEntityAsync(contributor);

            return contributorResult;
        }

        public async Task<bool> RemoveContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId, false);

            _imagesRepo.DeleteImage(contributor!.ContributorExcellence.Logo, null);

            var result = await _contributorRepo.RemoveEntityAsync(contributorId);

            return result;
        }

        public async Task<ContributorModelDto?> GetContributorByExcNameAsync(string name)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.ContributorExcellence.Name == name);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }

        public async Task<ContributorModelDto?> GetContributorByUserIdAsync(Guid userId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.UserId == userId);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }

        public async Task<PageResponse<ContributorModelDto?>> GetContributorsAsPageAsync(ContributorsFilter filter)
        {
            _contributorValidationRepo.CheckPaginationOptions(filter.PaginationInfo);

            var includesFilter = filter.ShowOnlyExcellences ? 
                new IncludeProperties<ContributorModel>(x => x.ContributorExcellence)
                : new IncludeProperties<ContributorModel>();

            var query = await _contributorRepo.GetManyEntitiesAsync(includesFilter);

            query = query
                .ApplyFilter(filter)
                .ApplyOrderBy(filter);

            var result = await _contributorRepo.GetMappedPageAsync<ContributorModelDto>(
                query, filter.PaginationInfo, _mapper);

            return result;
        }

        public async Task<ContributorConfirmInfoDto?> GetConfirmInfoByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId);
            
            if (contributor.ContributorConfirmInfo == null)
            {
                return null;
            }

            var result = _mapper.Map<ContributorConfirmInfoDto>(contributor.ContributorConfirmInfo);

            return result;
        }

        public async Task<bool> ChangeConfirmInfoForContributorAsync(Guid contributorId, ContributorConfirmInfoDto info)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId);

            if (contributor.ContributorConfirmInfo == null)
            {
                var createResult = await CreateConfirmInfForContributorAsync(info);
                return createResult.HasValue;
            }

            var contributorInfo = contributor.ContributorConfirmInfo;

            contributorInfo.IsConfirmed = info.IsConfirmed;
            contributorInfo.TimeStamp = DateTime.Now;

            var result = await _contributorConfirmInfoRepo.UpdateEntityAsync(contributorInfo);
            return result;

        }

        private async Task<Guid?> CreateConfirmInfForContributorAsync(ContributorConfirmInfoDto info)
        {
            var contributorInfo = new ContributorConfirmInfo
            {
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                IsConfirmed = info.IsConfirmed,
            };

            var result = await _contributorConfirmInfoRepo.CreateEntityAsync(contributorInfo);
            return result;
        }
    }
}
