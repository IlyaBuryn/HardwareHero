using Amazon.Runtime.Internal.Util;
using AutoMapper;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RegionService> _logger;

        public RegionService(
            IBaseRepositoryAsync<Region> regionRepo,
            IMapper mapper,
            ILogger<RegionService> logger)
        {
            _regionRepo = regionRepo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<RegionDto> AddRegionAsync(RegionDto regionToAdd)
        {
            await _regionRepo.AlreadyExistCheckAsync(
                x => x.Code == regionToAdd.Code ||
                x.Country == regionToAdd.Country);

            var region = _mapper.Map<Region>(regionToAdd);

            var result = await _regionRepo.CreateEntityAsync(region);
            result.DataAnswerCheck(true, _logger);

            return regionToAdd;
        }


        public async Task<RegionDto> UpdateRegionAsync(RegionDto regionToUpdate)
        {
            var region = await _regionRepo.NotFoundCheckAsync(
                x => x.Id == regionToUpdate.Id);

            if (region.Code != regionToUpdate.Code)
            {
                await _regionRepo.AlreadyExistCheckAsync(
                    x => x.Code == regionToUpdate.Code);
            }

            if (region.Country != regionToUpdate.Country)
            {
                await _regionRepo.AlreadyExistCheckAsync(
                    x => x.Country == regionToUpdate.Country);
            }

            var regionDto = _mapper.Map<Region>(regionToUpdate);

            var result = await _regionRepo.UpdateEntityAsync(regionDto);
            result.DataAnswerCheck(true, _logger);

            return regionToUpdate;
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
