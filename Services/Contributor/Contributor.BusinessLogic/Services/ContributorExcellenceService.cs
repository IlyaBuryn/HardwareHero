using Contributor.DataAccess.Models;
using Contributor.DTOs.Domain.Contributors;
using HardwareHero.Shared.Extensions.Repository;

namespace Contributor.BusinessLogic.Services
{
    public class ContributorExcellenceService : IContributorExcellenceService
    {
        private readonly IBaseRepositoryAsync<ContributorExcellence> _excellenceRepo;
        private readonly IBaseRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IFileRepositoryAsync _imageRepo;

        private readonly IMapper _mapper;

        public ContributorExcellenceService(
            IBaseRepositoryAsync<ContributorExcellence> excellenceRepo,
            IBaseRepositoryAsync<ContributorModel> contributorRepo,
            IFileRepositoryAsync imageRepo,
            IMapper mapper)
        {
            _excellenceRepo = excellenceRepo;
            _contributorRepo = contributorRepo;
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .NotFoundCheckAsync(x => x.Id == contributorId);

            var result = _mapper.Map<ContributorExcellenceDto>(contributor.ContributorExcellence);

            return result;
        }

        public async Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate)
        {
            var excellence = await _excellenceRepo
                .NotFoundCheckAsync(x => x.Id == excellenceToUpdate.Id);

            await _excellenceRepo.AlreadyExistCheckAsync(x => x.Name != excellenceToUpdate.Name);

            excellence.Phone = excellenceToUpdate.Phone;
            excellence.MainWebLink = excellenceToUpdate.MainWebLink;
            excellence.MainApiLink = excellenceToUpdate.MainApiLink;
            excellence.Description = excellenceToUpdate.Description;
            excellence.Name = excellenceToUpdate.Name;
            excellence.LogoUrl = excellenceToUpdate.LogoUrl;

            var imageName = excellence.LogoName;

            var replaceResult = await _imageRepo.ReplaceFileAsync(imageName,
                excellenceToUpdate.ImageData, excellenceToUpdate.LogoName);
            excellenceToUpdate.LogoUrl = replaceResult.Value;

            excellence.Currency = _mapper.Map<Currency>(excellenceToUpdate.Currency);
            excellence.Region = _mapper.Map<Region>(excellenceToUpdate.Region);

            var result = await _excellenceRepo.UpdateEntityAsync(excellence);
            result.DataAnswerCheck();

            return result.Value != null;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name)
        {
            var excellence = await _excellenceRepo
                .NotFoundCheckAsync(x => x.Name == name);

            var result = _mapper.Map<ContributorExcellenceDto>(excellence);
            
            return result;
        }
    }
}
