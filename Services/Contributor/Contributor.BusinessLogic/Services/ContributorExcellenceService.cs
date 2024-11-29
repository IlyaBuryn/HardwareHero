using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Contributors;
using EventDriven.Shared.Services;
using HardwareHero.Shared.Extensions.Repository;
using Storage.DTOs.Events;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorExcellenceService : IContributorExcellenceService
    {
        private readonly IBaseRepositoryAsync<ContributorExcellence> _excellenceRepo;
        private readonly IBaseRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IRequestService<UploadFileEvent, ReturnFileUrlEvent> _uploadFileService;
        private readonly IRequestService<ChangeFileEvent, ReturnFileUrlEvent> _changeFileService;
        private readonly IRequestService<DeleteFileEvent, DeleteFileResultEvent> _deleteFileService;

        private readonly IMapper _mapper;

        public ContributorExcellenceService(
            IBaseRepositoryAsync<ContributorExcellence> excellenceRepo,
            IBaseRepositoryAsync<ContributorModel> contributorRepo,
            IMapper mapper,
            IRequestService<UploadFileEvent, ReturnFileUrlEvent> uploadFileService,
            IRequestService<ChangeFileEvent, ReturnFileUrlEvent> changeFileService,
            IRequestService<DeleteFileEvent, DeleteFileResultEvent> deleteFileService)
        {
            _excellenceRepo = excellenceRepo;
            _contributorRepo = contributorRepo;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
            _changeFileService = changeFileService;
            _deleteFileService = deleteFileService;
        }


        public async Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId,
                x => x.ContributorExcellence);

            var result = _mapper.Map<ContributorExcellenceDto>(contributor.ContributorExcellence);

            return result;
        }


        public async Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate)
        {
            var excellence = await _excellenceRepo
                .NotFoundCheckAsync(x => x.Id == excellenceToUpdate.Id);

            await _excellenceRepo.AlreadyExistCheckAsync(x => x.Name != excellenceToUpdate.Name);

            excellence.Phone = excellenceToUpdate.Phone;
            excellence.MainWebLink = excellenceToUpdate.MainWebLink;
            excellence.MainApiLink = excellenceToUpdate.MainApiLink;
            excellence.Description = excellenceToUpdate.Description;
            excellence.Name = excellenceToUpdate.Name;

            if (excellenceToUpdate.ImageData != null)
            {
                var response = await _changeFileService.SendRequestAsync(new ChangeFileEvent()
                {
                    NewFile = excellenceToUpdate.ImageData,
                    NewFileName = string.Join('_', excellenceToUpdate.Id, excellence.Name),
                    OldFileName = string.Join('_', excellenceToUpdate.Id, excellence.Name),
                });

                excellence.LogoUrl = response.FileUrl;
            }

            excellence.Currency = _mapper.Map<Currency>(excellenceToUpdate.Currency);
            excellence.Region = _mapper.Map<Region>(excellenceToUpdate.Region);

            var result = await _excellenceRepo.UpdateEntityAsync(excellence);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name)
        {
            var excellence = await _excellenceRepo
                .NotFoundCheckAsync(x => x.Name == name);

            var result = _mapper.Map<ContributorExcellenceDto>(excellence);
            
            return result;
        }
    }
}
