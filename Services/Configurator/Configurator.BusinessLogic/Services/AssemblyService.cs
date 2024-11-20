using Configurator.BusinessLogic.Models;
using Configurator.DTOs.Domain;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using MongoDB.Driver;

namespace Configurator.BusinessLogic.Services
{
    public class AssemblyService : IAssemblyService
    {
        private readonly IBaseRepositoryAsync<StoredAssembly> _assemblyRepo;
        private readonly IMapper _mapper;

        public AssemblyService(
            IBaseRepositoryAsync<StoredAssembly> assemblyRepo,
            IMapper mapper)
        {
            _assemblyRepo = assemblyRepo;
            _mapper = mapper;
        }

        public async Task<List<StoredAssemblyDto?>> GetAssembliesByUserIdAsync(Guid userId)
        {
            var assemblies = await _assemblyRepo.FindAllEntitiesAsync(x => x.UserId == userId);
            assemblies.DataAnswerCheck();

            var result = _mapper.Map<List<StoredAssemblyDto?>>(assemblies.Value);

            return result;
        }

        public async void AddComponentAsync(Guid assemblyId, ConfiguratorComponent componentToAdd)
        {
            var assembly = await _assemblyRepo.NotFoundCheckAsync(
                x => x.Id == assemblyId);

            assembly.SelectedComponents.Add(componentToAdd);
            var result = await _assemblyRepo.UpdateEntityAsync(assembly);
        }

        public async void RemoveComponentAsync(Guid assemblyId, Guid componentId)
        {
            var assembly = await _assemblyRepo.NotFoundCheckAsync(
                x => x.Id == assemblyId);

            var toDelete = assembly.SelectedComponents.FirstOrDefault(x => x.Id == componentId);
            if (toDelete == null)
            {
                throw new NotFoundException(nameof(toDelete));
            }

            assembly.SelectedComponents.Remove(toDelete);
            var result = await _assemblyRepo.UpdateEntityAsync(assembly);
        }


        public async Task<Guid?> SaveAssemblyAsync(StoredAssemblyDto assemblyToAdd)
        {
            assemblyToAdd.Id = Guid.NewGuid();
            assemblyToAdd.Timestamp = DateTime.Now;

            var assembly = _mapper.Map<StoredAssembly>(assemblyToAdd);
            var result = await _assemblyRepo.CreateEntityAsync(assembly);

            return result.Value?.Id;
        }

        public async Task<bool> UpdateAssemblyAsync(StoredAssemblyDto assemblyToUpdate)
        {
            var assembly = await _assemblyRepo.NotFoundCheckAsync(x => x.Id == assemblyToUpdate.Id);

            assembly.SelectedComponents = _mapper.Map<List<ConfiguratorComponent>>(assemblyToUpdate.SelectedComponents);
            var result = await _assemblyRepo.UpdateEntityAsync(assembly);

            return result.Value != null;
        }

        public async Task<bool> RemoveAssemblyAsync(Guid assemblyId)
        {
            var assembly = await _assemblyRepo.NotFoundCheckAsync(x => x.Id == assemblyId);

            var result = await _assemblyRepo.RemoveEntityAsync(assemblyId);

            return result.Value != null;
        }
    }
}
