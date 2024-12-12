using AutoMapper;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using References.BusinessLogic.Contracts;
using References.BusinessLogic.Models;
using References.DTOs.Domain;
using References.DTOs.Messages;
using System.Linq.Expressions;

namespace References.BusinessLogic.Services
{
    public class RegionService : IRegionService
    {
        private readonly IBaseRepositoryAsync<Region> _regionRepo;
        private readonly IMapper _mapper;

        public RegionService(
            IBaseRepositoryAsync<Region> regionRepo,
            IMapper mapper)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
        }


        public async Task<Guid?> AddRegionAsync(RegionDto regionToAdd)
        {
            await _regionRepo.AlreadyExistCheckAsync(
                x => x.Code == regionToAdd.Code ||
                x.Country == regionToAdd.Country);

            var region = _mapper.Map<Region>(regionToAdd);

            var result = await _regionRepo.CreateEntityAsync(region);
            result.DataAnswerCheck();

            return region.Id;
        }


        public async Task<bool> UpdateRegionAsync(RegionDto regionToUpdate)
        {
            await _regionRepo.AlreadyExistCheckAsync(
                x => x.Code == regionToUpdate.Code ||
                x.Country == regionToUpdate.Country);

            await _regionRepo.NotFoundCheckAsync(
                x => x.Id == regionToUpdate.Id);

            var region = _mapper.Map<Region>(regionToUpdate);

            var result = await _regionRepo.UpdateEntityAsync(region);
            result.DataAnswerCheck();

            return result.Value != null;
        }


        public async Task<IEnumerable<RegionDto>?> GetRegionsAsync()
        {
            var regions = await _regionRepo.FindAllEntitiesAsync();
            regions.DataAnswerCheck();

            var mappedRegions = _mapper.Map<IEnumerable<RegionDto>>(regions.Value);

            return mappedRegions;
        }

        public async Task<RegionDto?> FindRegionAsync(RegionRequestMessage filter)
        {
            ArgumentNullException.ThrowIfNull(filter, nameof(filter));

            Expression<Func<Region, bool>>? predicate = filter.ByRegionId.HasValue
                ? x => x.Id == filter.ByRegionId
                : !string.IsNullOrEmpty(filter.ByCode)
                    ? x => x.Code == filter.ByCode
                    : !string.IsNullOrEmpty(filter.ByCountry)
                        ? x => x.Country == filter.ByCountry
                        : null;

            if (predicate == null)
            {
                return null;
            }

            var result = await _regionRepo.FindEntityAsync(predicate);

            return result.Value != null
                ? _mapper.Map<RegionDto>(result.Value)
                : null;
        }
    }
}
