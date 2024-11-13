using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using HardwareHero.Shared.Extensions.Repository;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentImagesService : IComponentImagesService
    {
        private readonly IBaseRepositoryAsync<ComponentImage> _componentImagesRepo;
        private readonly IBaseRepositoryAsync<Component> _componentRepo;
        private readonly IFileRepositoryAsync _imagesRepo;
        private readonly IMapper _mapper;


        public ComponentImagesService(
            IBaseRepositoryAsync<ComponentImage> componentImagesRepo,
            IFileRepositoryAsync imagesRepo,
            IMapper mapper,
            IBaseRepositoryAsync<Component> componentRepo)
        {
            _componentImagesRepo = componentImagesRepo;
            _imagesRepo = imagesRepo;
            _mapper = mapper;
            _componentRepo = componentRepo;
        }

        public async Task<Guid?> AddComponentImageAsync(ComponentImageDto componentImageToAdd)
        {
            componentImageToAdd.Id = Guid.NewGuid();
            var component = await _componentRepo.NotFoundCheckAsync(x => x.Id == componentImageToAdd.Id);
            
            var componentImage = _mapper.Map<ComponentImage>(componentImageToAdd);
            var imageLink = await _imagesRepo.UploadFileAsync(componentImageToAdd.ImageData,
                componentImage.ImageName!);
            imageLink.DataAnswerCheck();

            componentImage.ImageUrl = imageLink.Value;
            var result = await _componentImagesRepo.CreateEntityAsync(componentImage);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> RemoveComponentImageAsync(Guid componentImageId, bool removeActive = false)
        {
            var existImage = await _componentImagesRepo.NotFoundCheckAsync(x => x.Id == componentImageId);
            if (existImage.ComponentId != null && !removeActive)
            {
                return false;
            }

            var result = await _imagesRepo.DeleteFileAsync(existImage.ImageName);
            result.DataAnswerCheck();

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
