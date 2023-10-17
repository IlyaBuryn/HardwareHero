using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs.Aggregator;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using System.Linq.Expressions;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentImagesService : IComponentImagesService
    {
        private readonly ICrudRepositoryAsync<ComponentImages> _componentImagesRepo;
        private readonly IValidationRepository<ComponentImages> _componentImagesValidationRepo;
        private readonly IIMagesRepositoryAsync<ComponentImages> _imagesRepo;
        private readonly IMapper _mapper;

        public ComponentImagesService(
            ICrudRepositoryAsync<ComponentImages> componentImagesRepo,
            IValidationRepository<ComponentImages> componentImagesValidationRepo,
            IMapper mapper,
            IIMagesRepositoryAsync<ComponentImages> imagesRepo)
        {
            _componentImagesRepo = componentImagesRepo;
            _componentImagesValidationRepo = componentImagesValidationRepo;
            _mapper = mapper;
            _imagesRepo = imagesRepo;
        }

        public async Task<Guid?> AddComponentImageAsync(ComponentImagesDto componentImageToAdd)
        {
            componentImageToAdd.Id = Guid.NewGuid();

            _componentImagesValidationRepo.CheckIsAlreadyExist(
                x => x.ComponentId == componentImageToAdd.ComponentId &&
                x.Image == componentImageToAdd.Image,
                new AlreadyExistException(nameof(componentImageToAdd), componentImageToAdd.Image));

            var componentImage = _mapper.Map<ComponentImages>(componentImageToAdd);
            var imageSaveResult = await _imagesRepo.SaveImageAsync(componentImage, 
                componentImageToAdd.ImageData, ComponentImagesDto.GetFileNameExpression());

            Guid result = Guid.Empty;
            if (!string.IsNullOrEmpty(imageSaveResult))
            {
                result = await _componentImagesRepo.CreateEntityAsync(componentImage);
            }

            return result;
        }

        
        public async Task<bool> RemoveComponentImageAsync(Guid componentImageId)
        {
            var image = await _componentImagesRepo.GetOneEntityAsync(componentImageId);
            if (image == null)
            {
                throw new NotFoundException(nameof(componentImageId));
            }

            var imageDeleteResult = await _imagesRepo.DeleteImageAsync(image,
                ComponentImagesDto.GetFileNameExpression());

            bool result = false;
            if (!string.IsNullOrEmpty(imageDeleteResult))
            {
                result = await _componentImagesRepo.RemoveEntityAsync(componentImageId);
            }

            return result;
        }
    }
}
