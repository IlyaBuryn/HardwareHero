using Microsoft.Extensions.Options;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentImagesService : IComponentImagesService
    {
        private readonly ICrudRepositoryAsync<ComponentImages> _componentImagesRepo;
        private readonly IValidationRepository<ComponentImages> _componentImagesValidationRepo;
        private readonly IFileRepositoryAsync _imagesRepo;
        private readonly IMapper _mapper;
        private readonly string _fileNameDivider;


        public ComponentImagesService(
            ICrudRepositoryAsync<ComponentImages> componentImagesRepo,
            IValidationRepository<ComponentImages> componentImagesValidationRepo,
            IFileRepositoryAsync imagesRepo,
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

            //_componentImagesValidationRepo.CheckIfObjectAlreadyExist(
            //    x => x.ComponentId == componentImageToAdd.ComponentId && x.Image == componentImageToAdd.Image,
            //    componentImageToAdd.Image);
            
            var componentImage = _mapper.Map<ComponentImages>(componentImageToAdd);
            var imageLink = await _imagesRepo.UploadFileAsync(componentImageToAdd.ImageData,
                componentImage.ComponentId + _fileNameDivider + componentImage.Image);
            componentImage.Image = imageLink;

            Guid result = Guid.Empty;
            if (!string.IsNullOrEmpty(imageLink))
            {
                result = await _componentImagesRepo.CreateEntityAsync(componentImage);
            }

            return result;
        }


        public async Task<bool> RemoveComponentImageAsync(Guid componentImageId)
        {
            var image = await _componentImagesRepo.GetOneWithNotFoundCheck(x => x.Id == componentImageId);

            var imageId = image.Image.Split("id=").Last();
            var imageDeleteResult = await _imagesRepo.DeleteFileAsync(imageId);

            bool result = false;
            if (imageDeleteResult)
            {
                result = await _componentImagesRepo.RemoveEntityAsync(componentImageId);
            }

            return result;
        }
    }
}
