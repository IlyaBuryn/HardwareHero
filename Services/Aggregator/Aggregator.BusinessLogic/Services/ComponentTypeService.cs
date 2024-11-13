using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using HardwareHero.Shared.Extensions.Repository;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentTypeService : IComponentTypeService
    {
        private readonly IBaseRepositoryAsync<ComponentType> _componentTypeRepo;
        private readonly IBaseRepositoryAsync<Component> _componentRepo;

        private readonly IMapper _mapper;

        public ComponentTypeService(
            IBaseRepositoryAsync<ComponentType> componentTypeRepo,
            IBaseRepositoryAsync<Component> componentRepo,
            IMapper mapper)
        {
            _componentTypeRepo = componentTypeRepo;
            _mapper = mapper;
            _componentRepo = componentRepo;
        }

        public async Task<Guid?> AddComponentTypeAsync(ComponentTypeDto componentTypeToAdd)
        {
            componentTypeToAdd.Id = Guid.NewGuid();

            await _componentTypeRepo.AlreadyExistCheckAsync(x => x.Name == componentTypeToAdd.Name);

            var componentType = _mapper.Map<ComponentType>(componentTypeToAdd);
            var result = await _componentTypeRepo.CreateEntityAsync(componentType);
            result.DataAnswerCheck();

            return result?.Value?.Id;
        }


        public async Task<bool> UpdateComponentTypeAsync(ComponentTypeDto componentTypeToUpdate)
        {
            await _componentTypeRepo.AlreadyExistCheckAsync(
                x => x.Name == componentTypeToUpdate.Name && x.Id != componentTypeToUpdate.Id);

            var componentTypeAnswer = await _componentTypeRepo.FindEntityAsync(
                x => x.Id == componentTypeToUpdate.Id);
            componentTypeAnswer.DataAnswerCheck();

            var componentType = componentTypeAnswer.Value;
            componentType!.Name = componentTypeToUpdate.Name;
            componentType!.FullName = componentTypeToUpdate.FullName;
            componentType!.Description = componentTypeToUpdate.Description;
            componentType!.AccessibleForConfigurator = componentTypeToUpdate.AccessibleForConfigurator;

            var result = await _componentTypeRepo.UpdateEntityAsync(componentType);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<bool> RemoveComponentTypeAsync(Guid typeId)
        {
            await _componentTypeRepo.NotFoundCheckAsync(x => x.Id == typeId);

            var components = await _componentRepo.FindAllEntitiesAsync(x => x.ComponentTypeId == typeId);
            components.DataAnswerCheck();

            if (components.Value!.Count() > 0)
            {
                throw new DataValidationException(
                    $"Cannot delete an object because {components.Value!.Count()}" +
                    $" {nameof(Component)} objects depend on this object.");
            }

            var result = await _componentTypeRepo.RemoveEntityAsync(typeId);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<IEnumerable<ComponentTypeDto>?> GetComponentTypesAsync()
        {
            var componentTypes = await _componentTypeRepo.FindAllEntitiesAsync();
            componentTypes.DataAnswerCheck();

            var result = _mapper.Map<List<ComponentTypeDto?>>(componentTypes.Value);
            return result!;
        }
    }
}
