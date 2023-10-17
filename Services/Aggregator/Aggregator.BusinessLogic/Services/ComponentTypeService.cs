using Aggregator.BusinessLogic.Contracts;
using AutoMapper;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Aggregator.BusinessLogic.Services
{
    public class ComponentTypeService : IComponentTypeService
    {
        private readonly ICrudRepositoryAsync<ComponentType> _componentTypeRepo;
        private readonly IValidationRepository<ComponentType> _componentTypeValidationRepo;
        private readonly IMapper _mapper;

        public ComponentTypeService(
            ICrudRepositoryAsync<ComponentType> componentTypeRepo,
            IValidationRepository<ComponentType> componentTypeValidationRepo,
            IMapper mapper)
        {
            _componentTypeRepo = componentTypeRepo;
            _componentTypeValidationRepo = componentTypeValidationRepo;
            _mapper = mapper;
        }

        public async Task<Guid?> AddComponentTypeAsync(ComponentTypeDto componentTypeToAdd)
        {
            componentTypeToAdd.Id = Guid.NewGuid();
            _componentTypeValidationRepo.CheckIsAlreadyExist(x => x.Name == componentTypeToAdd.Name,
                new AlreadyExistException(nameof(componentTypeToAdd), componentTypeToAdd.Name));

            var componentType = _mapper.Map<ComponentType>(componentTypeToAdd);
            var result = await _componentTypeRepo.CreateEntityAsync(componentType);

            return result;
        }


        public async Task<bool> UpdateComponentTypeAsync(ComponentTypeDto componentTypeToUpdate)
        {
            _componentTypeValidationRepo.CheckIsNotFound(x => x.Id == componentTypeToUpdate.Id,
                new NotFoundException(nameof(ComponentType)));
            _componentTypeValidationRepo.CheckIsAlreadyExist(
                x => x.Name == componentTypeToUpdate.Name && x.Id != componentTypeToUpdate.Id,
                new AlreadyExistException(nameof(componentTypeToUpdate), componentTypeToUpdate.Name));

            var componentType = await _componentTypeRepo.GetOneEntityAsync(componentTypeToUpdate.Id);

            componentType.Name = componentTypeToUpdate.Name;
            componentType.FullName = componentTypeToUpdate.FullName;
            componentType.Description = componentTypeToUpdate.Description;

            var result = await _componentTypeRepo.UpdateEntityAsync(componentType);

            return result;
        }


        public async Task<bool> RemoveComponentTypeAsync(Guid typeId)
        {
            _componentTypeValidationRepo.CheckIsNotFound(x => x.Id == typeId,
                new NotFoundException(nameof(ComponentType)));
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
