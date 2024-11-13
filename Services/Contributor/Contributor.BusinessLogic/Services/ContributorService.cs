using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Contributors;
using HardwareHero.Shared.Extensions.Repository;
using System.Linq.Expressions;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IQueryRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IBaseRepositoryAsync<ContributorConfirmInfo> _contributorConfirmInfoRepo;
        private readonly IBaseRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly IBaseRepositoryAsync<ContributorExcellence> _excellenceRepo;
        
        private readonly IFileRepositoryAsync _imagesRepo;

        private readonly IMapper _mapper;

        public ContributorService(
            IQueryRepositoryAsync<ContributorModel> contributorRepo,
            IBaseRepositoryAsync<ContributorConfirmInfo> contributorConfirmInfoRepo,
            IBaseRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            IBaseRepositoryAsync<ContributorExcellence> excellenceRepo,
            IFileRepositoryAsync imagesRepo,
            IMapper mapper)
        {
            _contributorRepo = contributorRepo;
            _contributorConfirmInfoRepo = contributorConfirmInfoRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _excellenceRepo = excellenceRepo;
            _imagesRepo = imagesRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> SignUpContributorAsync(ContributorModelDto contributorToAdd)
        {
            contributorToAdd.Id = Guid.NewGuid();

            await _contributorRepo.AlreadyExistCheckAsync(x => x.UserId == contributorToAdd.UserId);
            await _excellenceRepo.AlreadyExistCheckAsync(
                x => x.Name == contributorToAdd.ContributorExcellence.Name);

            if (contributorToAdd.ContributorExcellence.ImageData != null)
            {
                var uploadResult = await _imagesRepo.UploadFileAsync(
                    contributorToAdd.ContributorExcellence.ImageData,
                    contributorToAdd.ContributorExcellence.LogoName);

                contributorToAdd.ContributorExcellence.LogoUrl = uploadResult.Value;
            }

            var contributor = _mapper.Map<ContributorModel>(contributorToAdd);
            var result = await _contributorRepo.CreateEntityAsync(contributor);
            result.DataAnswerCheck();

            return result.Value!.Id;
        }

        public async Task<bool> RemoveContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);

            var imageName = contributor.ContributorExcellence.LogoName;
            await _imagesRepo.DeleteFileAsync(imageName);

            var result = await _contributorRepo.RemoveEntityAsync(contributorId);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<ContributorModelDto?> GetContributorByExcNameAsync(string name)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.ContributorExcellence.Name == name);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }

        public async Task<ContributorModelDto?> GetContributorByUserIdAsync(Guid userId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.UserId == userId);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }

        // TODO: remove switch from filter
        public async Task<PageResponse<ContributorModelDto?>> GetContributorsAsPageAsync(ContributorsFilter filter)
        {
            var query = await _contributorRepo.FindPagedAsync(null, filter,
                x => x.ContributorExcellence, 
                x => x.SubscriptionPlanInfo, 
                x => x.ContributorConfirmInfo);

            //query = query.ApplyFilter(filter).Query;
            //query = query.ApplyOrderBy(filter).Query;

            var page = query.ToPageResponse;
            var mappedResult = _mapper.Map<PageResponse<ContributorModelDto?>>(page);

            return mappedResult;
        }

        public async Task<ContributorConfirmInfoDto?> GetConfirmInfoByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);
            
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
                .NotFoundCheckAsync(x => x.Id == contributorId);

            if (contributor.ContributorConfirmInfo == null)
            {
                var createResult = await CreateConfirmInfForContributorAsync(info);
                return createResult.HasValue;
            }

            var contributorInfo = contributor.ContributorConfirmInfo;

            contributorInfo.IsConfirmed = info.IsConfirmed;
            contributorInfo.TimeStamp = DateTime.Now;

            var result = await _contributorConfirmInfoRepo.UpdateEntityAsync(contributorInfo);
            result.DataAnswerCheck();

            return result.Value != null;

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
            result.DataAnswerCheck();

            return result.Value!.Id;
        }
    }
}
