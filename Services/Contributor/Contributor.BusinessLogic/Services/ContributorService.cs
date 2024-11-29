using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Contributors;
using EventDriven.Shared.Services;
using HardwareHero.Shared.Extensions.Repository;
using Identity.Shared.Events;
using Mail.DTOs.Events;
using Storage.DTOs.Events;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorService : IContributorService
    {
        private readonly IQueryRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IBaseRepositoryAsync<ContributorConfirmInfo> _contributorConfirmInfoRepo;
        private readonly IBaseRepositoryAsync<SubscriptionPlan> _subscriptionPlanRepo;
        private readonly IBaseRepositoryAsync<ContributorExcellence> _excellenceRepo;

        private readonly IRequestService<UploadFileEvent, ReturnFileUrlEvent> _uploadFileService;
        private readonly IRequestService<DeleteFileEvent, DeleteFileResultEvent> _deleteFileService;
        private readonly IProducerService<FindUserToMailSagaEvent> _mailSaga;

        private readonly IMapper _mapper;

        public ContributorService(
            IQueryRepositoryAsync<ContributorModel> contributorRepo,
            IBaseRepositoryAsync<ContributorConfirmInfo> contributorConfirmInfoRepo,
            IBaseRepositoryAsync<SubscriptionPlan> subscriptionPlanRepo,
            IBaseRepositoryAsync<ContributorExcellence> excellenceRepo,
            IMapper mapper,
            IRequestService<UploadFileEvent, ReturnFileUrlEvent> uploadFileService,
            IRequestService<DeleteFileEvent, DeleteFileResultEvent> deleteFileService,
            IProducerService<FindUserToMailSagaEvent> mailSaga)
        {
            _contributorRepo = contributorRepo;
            _contributorConfirmInfoRepo = contributorConfirmInfoRepo;
            _subscriptionPlanRepo = subscriptionPlanRepo;
            _excellenceRepo = excellenceRepo;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
            _deleteFileService = deleteFileService;
            _mailSaga = mailSaga;
        }


        public async Task<Guid?> SignUpContributorAsync(ContributorModelDto contributorToAdd)
        {
            contributorToAdd.Id = Guid.NewGuid();

            await _contributorRepo.AlreadyExistCheckAsync(x => x.UserId == contributorToAdd.UserId);
            await _excellenceRepo.AlreadyExistCheckAsync(
                x => x.Name == contributorToAdd.ContributorExcellence.Name);

            if (contributorToAdd.ContributorExcellence.ImageData != null)
            {
                var uploadResult = await _uploadFileService.SendRequestAsync(
                    new UploadFileEvent()
                    {
                        FileName = string.Join('_', contributorToAdd.Id, contributorToAdd.ContributorExcellence.Name),
                        File = contributorToAdd.ContributorExcellence.ImageData
                    });

                contributorToAdd.ContributorExcellence.LogoUrl = uploadResult.FileUrl;
            }

            var contributor = _mapper.Map<ContributorModel>(contributorToAdd);
            var result = await _contributorRepo.CreateEntityAsync(contributor);
            result.DataAnswerCheck();

            await _mailSaga.ProduceAsync(new FindUserToMailSagaEvent()
            {
                UserId = contributorToAdd.UserId.ToString(),
                Message = new SendMailEvent()
                {
                    MailPreset = Mail.DTOs.MailPreset.SignUpContributor,
                }

            }, default);

            return result.Value!.Id;
        }


        public async Task<bool> RemoveContributorAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);

            var imageName = string.Join('_', contributor.Id, contributor.ContributorExcellence.Name);
            await _deleteFileService.SendRequestAsync(new DeleteFileEvent() { FileName = imageName });

            var result = await _contributorRepo.RemoveEntityAsync(contributorId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<ContributorModelDto?> GetContributorByNameAsync(string name)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.ContributorExcellence.Name == name,
                x => x.ContributorConfirmInfo!,
                x => x.SubscriptionPlanInfo!,
                x => x.ContributorExcellence);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }


        public async Task<ContributorModelDto?> GetContributorByUserIdAsync(Guid userId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.UserId == userId,
                x => x.ContributorConfirmInfo!,
                x => x.SubscriptionPlanInfo!,
                x => x.ContributorExcellence);

            var result = _mapper.Map<ContributorModelDto>(contributor);
            return result;
        }


        public async Task<PageResponse<ContributorModelDto?>> GetContributorsAsPageAsync(ContributorsFilter filter)
        {
            var query = await _contributorRepo.FindPagedAsync(
                filter.BuildFilterPredicate(), filter,
                x => x.ContributorExcellence, 
                x => x.SubscriptionPlanInfo!, 
                x => x.ContributorConfirmInfo!);

            var page = query.ToPageResponse;
            var mappedResult = _mapper.Map<PageResponse<ContributorModelDto?>>(page);

            return mappedResult;
        }


        public async Task<ContributorConfirmInfoDto?> GetConfirmInfoByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId,
                x => x.ContributorConfirmInfo!);
            
            if (contributor.ContributorConfirmInfo == null)
            {
                return null;
            }

            var result = _mapper.Map<ContributorConfirmInfoDto>(contributor.ContributorConfirmInfo);

            return result;
        }


        public async Task<bool> ChangeContributorConfirmInfoAsync(Guid contributorId, ContributorConfirmInfoDto info)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId,
                x => x.ContributorConfirmInfo!);

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
