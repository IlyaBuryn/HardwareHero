using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Extensions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentTypeService : IComponentTypeService
    {
        private readonly ICrudRepositoryAsync<ComponentType> _componentTypeRepo;
        private readonly ICrudRepositoryAsync<Component> _componentRepo;

        private readonly IValidationRepository<ComponentType> _componentTypeValidationRepo;

        private readonly IMapper _mapper;

        public ComponentTypeService(
            ICrudRepositoryAsync<ComponentType> componentTypeRepo,
            IValidationRepository<ComponentType> componentTypeValidationRepo,
            IMapper mapper,
            ICrudRepositoryAsync<Component> componentRepo)
        {
            _componentTypeRepo = componentTypeRepo;
            _componentTypeValidationRepo = componentTypeValidationRepo;
            _mapper = mapper;
            _componentRepo = componentRepo;
        }

        public async Task<Guid?> AddComponentTypeAsync(ComponentTypeDto componentTypeToAdd)
        {
            componentTypeToAdd.Id = Guid.NewGuid();

            _componentTypeValidationRepo.CheckIfObjectAlreadyExist(x => x.Name == componentTypeToAdd.Name,
                componentTypeToAdd.Name);

            var componentType = _mapper.Map<ComponentType>(componentTypeToAdd);
            var result = await _componentTypeRepo.CreateEntityAsync(componentType);

            return result;
        }


        public async Task<bool> UpdateComponentTypeAsync(ComponentTypeDto componentTypeToUpdate)
        {
            _componentTypeValidationRepo.CheckIfObjectAlreadyExist(
                x => x.Name == componentTypeToUpdate.Name && x.Id != componentTypeToUpdate.Id,
                componentTypeToUpdate.Name);

            var componentType = await _componentTypeRepo.GetOneWithNotFoundCheck(x => x.Id == componentTypeToUpdate.Id);

            componentType.Name = componentTypeToUpdate.Name;
            componentType.FullName = componentTypeToUpdate.FullName;
            componentType.Description = componentTypeToUpdate.Description;

            var result = await _componentTypeRepo.UpdateEntityAsync(componentType);

            return result;
        }


        public async Task<bool> RemoveComponentTypeAsync(Guid typeId)
        {
            _componentTypeValidationRepo.CheckIfObjectNotFound(x => x.Id == typeId);

            var components = await _componentRepo.GetManyEntitiesAsync(x => x.ComponentTypeId == typeId);
            if (components.Count() > 0)
            {
                throw new DataValidationException(
                    $"Cannot delete an object because {components.Count()} {nameof(Component)} objects depend on this object.");
            }

            var result = await _componentTypeRepo.RemoveEntityAsync(typeId);

            return result;
        }


        public async Task<List<ComponentTypeDto?>> GetComponentTypesAsync()
        {
            var componentTypes = await _componentTypeRepo.GetManyEntitiesAsync();

            var result = _mapper.Map<List<ComponentTypeDto?>>(componentTypes);
            return result;
        }
    }
}
