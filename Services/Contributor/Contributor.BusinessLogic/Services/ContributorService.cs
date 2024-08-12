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
        
        private readonly IFileRepositoryAsync _imagesRepo;

        private readonly IMapper _mapper;

        public ContributorService(
            ICollectionRepositoryAsync<ContributorModel> contributorRepo,
            IValidationRepository<ContributorModel> contributorValidationRepo,
            IValidationRepository<ContributorExcellence> contributorExcValidationRepo,
            ICrudRepositoryAsync<ContributorConfirmInfo> contributorConfirmInfoRepo,
            ICrudRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            ICrudRepositoryAsync<ContributorExcellence> excellenceRepo,
            IFileRepositoryAsync imagesRepo,
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

            var uploadResult = await _imagesRepo.UploadFileAsync(
                contributorToAdd.ContributorExcellence.ImageData,
                contributorToAdd.ContributorExcellence.Logo);

            contributorToAdd.ContributorExcellence.Logo = uploadResult;

            var contributor = _mapper.Map<ContributorModel>(contributorToAdd);
            var contributorResult = await _contributorRepo.CreateEntityAsync(contributor);

            return contributorResult;
        }

        public async Task<bool> RemoveContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId, false);

            var imageId = contributor.ContributorExcellence.Logo.Split("id=").Last();

            await _imagesRepo.DeleteFileAsync(imageId);

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
            _contributorValidationRepo.CheckPaginationOptions(filter);

            IncludeProperties<ContributorModel> includesFilter = filter.ShowOnlyExcellences ? 
                new(x => x.ContributorExcellence)
                : new(x => x.ContributorExcellence, x => x.SubscriptionPlanInfo, x => x.ContributorConfirmInfo);

            var query = await _contributorRepo.GetManyEntitiesAsync(includesFilter);

            query = query.ApplyFilter(filter).Query;
            query = query.ApplyOrderBy(filter).Query;

            var result = await _contributorRepo.GetMappedPageAsync(query, filter);
            var mappedResult = _mapper.Map<PageResponse<ContributorModelDto?>>(result);

            return mappedResult;
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
