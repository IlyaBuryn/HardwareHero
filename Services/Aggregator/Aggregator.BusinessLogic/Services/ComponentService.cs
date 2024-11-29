using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using EventDriven.Shared.Services;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Answers;
using Storage.DTOs.Events;
using static Aggregator.DTOs.Response.AggregatorResponseRecords;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentService : IComponentService
    {
        private readonly IQueryRepositoryAsync<Component> _componentRepo;
        private readonly IBaseRepositoryAsync<ComponentMetric> _componentMetricsRepo;
        private readonly IBaseRepositoryAsync<ComponentType> _componentTypeRepo;

        private readonly IRequestService<UploadFileEvent, ReturnFileUrlEvent> _uploadFileService;
        private readonly IRequestService<ChangeFileEvent, ReturnFileUrlEvent> _changeFileService;
        private readonly IRequestService<DeleteFileEvent, DeleteFileResultEvent> _deleteFileService;

        private readonly IMapper _mapper;

        public ComponentService(
            IQueryRepositoryAsync<Component> componentRepo,
            IBaseRepositoryAsync<ComponentMetric> componentMetricsRepo,
            IBaseRepositoryAsync<ComponentType> componentTypeRepo,
            IMapper mapper,
            IRequestService<UploadFileEvent, ReturnFileUrlEvent> uploadFileService,
            IRequestService<ChangeFileEvent, ReturnFileUrlEvent> changeFileService,
            IRequestService<DeleteFileEvent, DeleteFileResultEvent> deleteFileService)
        {
            _componentRepo = componentRepo;
            _componentMetricsRepo = componentMetricsRepo;
            _componentTypeRepo = componentTypeRepo;
            _mapper = mapper;
            _uploadFileService = uploadFileService;
            _changeFileService = changeFileService;
            _deleteFileService = deleteFileService;
        }


        public async Task<Guid?> AddComponentAsync(ComponentDto componentToAdd)
        {
            componentToAdd.Id = Guid.NewGuid();

            await _componentRepo.AlreadyExistCheckAsync(x => x.Name == componentToAdd.Name);
            await _componentTypeRepo.NotFoundCheckAsync(x => x.Id == componentToAdd.ComponentTypeId);

            if (componentToAdd.ComponentImages != null && componentToAdd.ComponentImages.Count() != 0)
            {
                var index = 0;
                foreach (var image in componentToAdd.ComponentImages)
                {
                    var response = await _uploadFileService.SendRequestAsync(
                        new UploadFileEvent()
                        {
                            File = image.ImageData,
                            FileName = string.Join('_', componentToAdd.Id, index)
                        });

                    image.ImageUrl = response.FileUrl;
                    image.Index = index;
                    index++;
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


        // TODO: This method doesn't update images
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


        // TODO: event to prices.api
        public async Task<ComponentDto?> GetComponentByIdAsync(Guid componentId)
        {
            var component = await _componentRepo.NotFoundCheckAsync(x => x.Id == componentId,
                x => x.ComponentMetric!,
                x => x.ComponentImages!,
                x => x.ComponentAttributes!,
                x => x.ComponentType!);

            await IncrementComponentViewCount(componentId);

            return _mapper.Map<ComponentDto?>(component);
        }


        // TODO: event to prices.api
        public async Task<IQueryable<ComponentDto?>> GetComponentsByIdsAsync(List<Guid> componentsIds)
        {
            var result = await _componentRepo.FindAsync(x => x.Equals(componentsIds.Any()),
                x => x.ComponentType!,
                x => x.ComponentImages!,
                x => x.ComponentMetric!);
            result.DataAnswerCheck();

            var mapped = _mapper.Map<IQueryable<ComponentDto>>(result.Value);

            return mapped;
        }


        // TODO: event to prices.api
        public async Task<PageResponse<ComponentDto>?> GetComponentsPageAsync(ComponentsFilter filter)
        {
            var page = await _componentRepo.FindPagedAsync(
                filter.BuildFilterPredicate(), filter,
                x => x.ComponentType!,
                x => x.ComponentImages!,
                x => x.ComponentMetric!);
            page.DataAnswerCheck();

            var result = page.ToPageResponse();
            var mapped = _mapper.Map<PageResponse<ComponentDto>>(result);

            return mapped;
        }


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
