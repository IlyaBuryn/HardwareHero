using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using EventDriven.Shared.Services;
using HardwareHero.Shared.Extensions.Repository;
using Storage.DTOs.Events;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentImagesService : IComponentImagesService
    {
        private readonly IBaseRepositoryAsync<ComponentImage> _componentImagesRepo;
        private readonly IBaseRepositoryAsync<Component> _componentRepo;

        private readonly IRequestService<UploadFileEvent, ReturnFileUrlEvent> _uploadFileService;
        private readonly IRequestService<ChangeFileEvent, ReturnFileUrlEvent> _changeFileService;
        private readonly IRequestService<DeleteFileEvent, DeleteFileResultEvent> _deleteFileService;

        private readonly IMapper _mapper;

        public ComponentImagesService(
            IBaseRepositoryAsync<ComponentImage> componentImagesRepo,
            IMapper mapper,
            IBaseRepositoryAsync<Component> componentRepo,
            IRequestService<UploadFileEvent, ReturnFileUrlEvent> uploadFileService,
            IRequestService<ChangeFileEvent, ReturnFileUrlEvent> changeFileService,
            IRequestService<DeleteFileEvent, DeleteFileResultEvent> deleteFileService)
        {
            _componentImagesRepo = componentImagesRepo;
            _mapper = mapper;
            _componentRepo = componentRepo;
            _uploadFileService = uploadFileService;
            _changeFileService = changeFileService;
            _deleteFileService = deleteFileService;
        }

        // TODO: FindInactiveImagesAsync();


        // TODO: Index problem
        public async Task<Guid?> AddComponentImageAsync(ComponentImageDto componentImageToAdd)
        {
            componentImageToAdd.Id = Guid.NewGuid();
            var component = await _componentRepo.NotFoundCheckAsync(x => x.Id == componentImageToAdd.Id);
            
            var componentImage = _mapper.Map<ComponentImage>(componentImageToAdd);
            var response = await _uploadFileService.SendRequestAsync(
                    new UploadFileEvent()
                    {
                        File = componentImageToAdd.ImageData,
                        FileName = string.Join('_', component.Id, componentImageToAdd.Index)
                    });

            componentImage.ImageUrl = response.FileUrl;
            var result = await _componentImagesRepo.CreateEntityAsync(componentImage);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> RemoveComponentImageAsync(Guid componentImageId, bool diactivate = false)
        {
            var existImage = await _componentImagesRepo.NotFoundCheckAsync(x => x.Id == componentImageId);
            if (existImage.ComponentId != null && !diactivate)
            {
                return false;
            }

            var response = await _deleteFileService.SendRequestAsync(
                new DeleteFileEvent()
                {
                    FileName = string.Join('_', existImage.ComponentId, existImage.Index)
                });

            var final = await _componentImagesRepo.RemoveEntityAsync(componentImageId);
            final.DataAnswerCheck();
            
            return final.Value != null;
        }


        public async Task<bool> ChangeActiveImageStatusAsync(Guid componentImageId)
        {
            var existImage = await _componentImagesRepo.NotFoundCheckAsync(x => x.Id == componentImageId);

            var id = existImage.ComponentId != null ? existImage.ComponentId : existImage.RevokedId;
            existImage.ComponentId = existImage.ComponentId == null ? id : null;
            existImage.RevokedId = existImage.RevokedId == null ? id : null;

            var result = await _componentImagesRepo.UpdateEntityAsync(existImage);
            result.DataAnswerCheck();
            
            return result.Value != null;
        }
    }
}
