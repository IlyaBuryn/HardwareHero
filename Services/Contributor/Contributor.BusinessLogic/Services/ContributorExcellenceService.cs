using AutoMapper;
using Contributor.BusinessLogic.Contracts;
using HardwareHero.Services.Shared.DTOs.Contributor;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Contributor;
using HardwareHero.Services.Shared.Repositories.Contracts;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorExcellenceService : IContributorExcellenceService
    {
        private readonly ICrudRepositoryAsync<ContributorExcellence> _excellenceRepo;
        private readonly IMapper _mapper;

        public ContributorExcellenceService(
            ICrudRepositoryAsync<ContributorExcellence> excellenceRepo,
            IMapper mapper)
        {
            _excellenceRepo = excellenceRepo;
            _mapper = mapper;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name)
        {
            var excellence = await _excellenceRepo.GetOneEntityAsync(
                expression: x => x.Name == name);
            if (excellence == null)
            {
                throw new NotFoundException(nameof(excellence));
            }

            var result = _mapper.Map<ContributorExcellenceDto>(excellence);
            
            return result;
        }

        public async Task<List<string>> GetExcellenceNamesAsync()
        {
            var excellencesCheck = await _excellenceRepo.GetManyEntitiesAsync();
            var result = new List<string>();

            if (excellencesCheck == null)
            {
                throw new NotFoundException(nameof(excellencesCheck));
            }

            foreach (var item in excellencesCheck)
            {
                if (item == null)
                {
                    continue;
                }

                result.Add(item.Name);
            }

            return result;
        }

        public async Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate)
        {
            var excellenceCheck = await _excellenceRepo.GetOneEntityAsync(
                expression: x => x.Id == excellenceToUpdate.Id);
            if (excellenceCheck == null)
            {
                throw new NotFoundException(nameof(excellenceCheck));
            }

            var excellenceName = await _excellenceRepo.GetOneEntityAsync(
                expression: x => x.Name == excellenceToUpdate.Name);
            if (excellenceName != null)
            {
                throw new AlreadyExistException("company", excellenceToUpdate.Name);
            }

            excellenceCheck.Name = excellenceToUpdate.Name;
            excellenceCheck.Logo = excellenceToUpdate.Logo;

            var result = await _excellenceRepo.UpdateEntityAsync(excellenceCheck);
            
            return result;
        }
    }
}
