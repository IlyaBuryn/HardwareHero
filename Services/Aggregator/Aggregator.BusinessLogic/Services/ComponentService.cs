using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Answers;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IQueryRepositoryAsync<Component> _componentRepo;
        private readonly IBaseRepositoryAsync<ComponentMetric> _componentMetricsRepo;
        private readonly IBaseRepositoryAsync<ComponentType> _componentTypeRepo;
        private readonly IFileRepositoryAsync _imagesRepo;

        private readonly IMapper _mapper;

        public ComponentService(
            IQueryRepositoryAsync<Component> componentRepo,
            IBaseRepositoryAsync<ComponentMetric> componentMetricsRepo,
            IBaseRepositoryAsync<ComponentType> componentTypeRepo,
            IFileRepositoryAsync imagesRepo,
            IMapper mapper)
        {
            _componentRepo = componentRepo;
            _componentMetricsRepo = componentMetricsRepo;
            _componentTypeRepo = componentTypeRepo;
            _imagesRepo = imagesRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddComponentAsync(ComponentDto componentToAdd)
        {
            componentToAdd.Id = Guid.NewGuid();

            await _componentRepo.AlreadyExistCheckAsync(x => x.Name == componentToAdd.Name);
            await _componentTypeRepo.NotFoundCheckAsync(x => x.Id == componentToAdd.ComponentTypeId);

            // TODO: Maybe I should just set images state here?
            if (componentToAdd.ComponentImages != null && componentToAdd.ComponentImages.Count() != 0)
            {

                foreach (var image in componentToAdd.ComponentImages)
                {
                    image.Id = Guid.NewGuid();
                    var imageName = $"{image.ComponentId}_{image.Id}";
                    var uploadingResult = await _imagesRepo.UploadFileAsync(image.ImageData, imageName);
                    image.ImageName = imageName;
                }
            }

            var metric = await _componentMetricsRepo.CreateEntityAsync(
                new ComponentMetric()
                {
                    CreatedAt = DateTime.UtcNow,
                });
            metric.DataAnswerCheck();

            var component = _mapper.Map<Component>(componentToAdd);
            var result = await _componentRepo.CreateEntityAsync(component);
            result.DataAnswerCheck();

            return result.Value?.Id;
        }


        public async Task<bool> UpdateComponentAsync(ComponentDto componentToUpdate)
        {
            await _componentRepo.AlreadyExistCheckAsync(
                x => x.Name == componentToUpdate.Name && x.Id != componentToUpdate.Id);

            var component = await _componentRepo
                .NotFoundCheckAsync(x => x.Id == componentToUpdate.Id);

            var componentType = await _componentTypeRepo
                .NotFoundCheckAsync(x => x.Id == componentToUpdate.ComponentTypeId);

            component.Name = componentToUpdate.Name;
            component.Description = componentToUpdate.Description;
            component.ComponentTypeId = componentType.Id;

            var result = await _componentRepo.UpdateEntityAsync(component);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveComponentAsync(Guid componentId, bool deactivate = true)
        {
            var component = await _componentRepo.NotFoundCheckAsync(x => x.Id == componentId);

            DataAnswer<Component> result;
            if (deactivate)
            {
                component.IsActive = false;
                result = await _componentRepo.UpdateEntityAsync(component);
            }
            else
            {
                result = await _componentRepo.RemoveEntityAsync(componentId);
            }

            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<CreationOfManyResponse> AddComponentsAsync(IEnumerable<ComponentDto> componentsToAdd)
        {
            var values = new Dictionary<string, string>();

            foreach (var componentDto in componentsToAdd)
            {
                try
                {
                    await AddComponentAsync(componentDto);
                    values.Add(componentDto.Name, true.ToString());
                }
                catch (Exception ex)
                {
                    values.Add(componentDto.Name, ex.Message);
                }
            }

            return new CreationOfManyResponse(values);
        }


        public async Task<ComponentDto?> GetComponentByIdAsync(Guid componentId)
        {
            var component = await _componentRepo.NotFoundCheckAsync(x => x.Id == componentId);

            await IncrementComponentViewCount(componentId);

            return _mapper.Map<ComponentDto?>(component);
        }


        public async Task<IQueryable<ComponentDto?>> GetComponentsByIdsAsync(List<Guid> componentsIds)
        {
            var result = await _componentRepo.FindAsync(x => x.Equals(componentsIds.Any()));
            result.DataAnswerCheck();

            var mapped = _mapper.Map<IQueryable<ComponentDto>>(result.Value);

            return mapped;
        }

        // TODO: 🤨
        public async Task<PageResponse<ComponentDto>?> GetComponentsPageAsync(ComponentsFilter filter)
        {
            var page = await _componentRepo.FindPagedAsync(null, filter);
            page.DataAnswerCheck();

            //query = query.ApplyFilter(filter).Query;
            //query = query.ApplyOrderBy(filter).Query;
            //var selected = query.ApplySelection(filter).Query;

            var result = page.ToPageResponse();
            var mapped = _mapper.Map<PageResponse<ComponentDto>>(result);

            return mapped;
        }

        // TODO: 🤨
        private async Task IncrementComponentViewCount(Guid componentId)
        {
            var componentMetric = await _componentMetricsRepo
                .FindEntityAsync(x => x.ComponentId == componentId);

            if (componentMetric.Value == null)
            {
                await _componentMetricsRepo.CreateEntityAsync(new ComponentMetric
                {
                    ComponentId = componentId,
                    ViewsCount = 1,
                });

                return;
            }

            var metric = componentMetric.Value;
            metric.ViewsCount += 1;
            await _componentMetricsRepo.UpdateEntityAsync(metric);
        }
    }
}
