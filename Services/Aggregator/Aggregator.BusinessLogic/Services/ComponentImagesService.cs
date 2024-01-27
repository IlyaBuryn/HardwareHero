using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.Extensions.Options;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentImagesService : IComponentImagesService
    {
        private readonly ICrudRepositoryAsync<ComponentImages> _componentImagesRepo;

        private readonly IValidationRepository<ComponentImages> _componentImagesValidationRepo;

        private readonly IObjectImageRepositoryAsync<ComponentImages> _imagesRepo;

        private readonly IMapper _mapper;

        private readonly string _fileNameDivider;


        public ComponentImagesService(
            ICrudRepositoryAsync<ComponentImages> componentImagesRepo,
            IValidationRepository<ComponentImages> componentImagesValidationRepo,
            IObjectImageRepositoryAsync<ComponentImages> imagesRepo,
            IOptions<ImagesSaveOptions> savePathOptions,
            IMapper mapper)
        {
            _componentImagesRepo = componentImagesRepo;
            _componentImagesValidationRepo = componentImagesValidationRepo;
            _imagesRepo = imagesRepo;
            _fileNameDivider = savePathOptions.Value.FileNameDivider ?? string.Empty;
            _mapper = mapper;
        }

        public async Task<Guid?> AddComponentImageAsync(ComponentImagesDto componentImageToAdd)
        {
            componentImageToAdd.Id = Guid.NewGuid();

            _componentImagesValidationRepo.CheckIfObjectAlreadyExist(
                x => x.ComponentId == componentImageToAdd.ComponentId && x.Image == componentImageToAdd.Image,
                componentImageToAdd.Image);

            var componentImage = _mapper.Map<ComponentImages>(componentImageToAdd);
            var imageSaveResult = await _imagesRepo.SaveImageAsync(componentImage, 
                componentImageToAdd.ImageData, componentImage.ComponentId + _fileNameDivider + componentImage.Image);

            Guid result = Guid.Empty;
            if (!string.IsNullOrEmpty(imageSaveResult))
            {
                result = await _componentImagesRepo.CreateEntityAsync(componentImage);
            }

            return result;
        }

        
        public async Task<bool> RemoveComponentImageAsync(Guid componentImageId)
        {
            var image = await _componentImagesRepo.GetOneWithNotFoundCheck(x => x.Id == componentImageId);

            var imageDeleteResult = await _imagesRepo.DeleteImageAsync(image,
                image.ComponentId + _fileNameDivider + image.Image);

            bool result = false;
            if (!string.IsNullOrEmpty(imageDeleteResult))
            {
                result = await _componentImagesRepo.RemoveEntityAsync(componentImageId);
            }

            return result;
        }
    }
}
