namespace Contributor.BusinessLogic.Services
{
    public class ContributorExcellenceService : IContributorExcellenceService
    {
        private readonly ICrudRepositoryAsync<ContributorExcellence> _excellenceRepo;
        private readonly ICrudRepositoryAsync<ContributorModel> _contributorRepo;

        private readonly IValidationRepository<ContributorExcellence> _excellenceValidRepo;

        private readonly IFileRepositoryAsync _imageRepo;

        private readonly IMapper _mapper;

        public ContributorExcellenceService(
            ICrudRepositoryAsync<ContributorExcellence> excellenceRepo,
            ICrudRepositoryAsync<ContributorModel> contributorRepo,
            IValidationRepository<ContributorExcellence> excellenceValidRepo,
            IFileRepositoryAsync imageRepo,
            IMapper mapper)
        {
            _excellenceRepo = excellenceRepo;
            _contributorRepo = contributorRepo;
            _excellenceValidRepo = excellenceValidRepo;
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByContributorIdAsync(Guid contributorId)
        {
            var contributor = await _contributorRepo
                .GetOneWithNotFoundCheck(x => x.Id == contributorId);

            var result = _mapper.Map<ContributorExcellenceDto>(contributor.ContributorExcellence);

            return result;
        }

        public async Task<bool> UpdateExcellenceAsync(ContributorExcellenceDto excellenceToUpdate)
        {
            var excellence = await _excellenceRepo
                .GetOneWithNotFoundCheck(x => x.Id == excellenceToUpdate.Id);

            _excellenceValidRepo.CheckIfObjectAlreadyExist(x => x.Name != excellenceToUpdate.Name);

            excellence.Phone = excellenceToUpdate.Phone;
            excellence.MainWebLink = excellenceToUpdate.MainWebLink;
            excellence.MainApiLink = excellenceToUpdate.MainApiLink;
            excellence.Description = excellenceToUpdate.Description;
            excellence.Name = excellenceToUpdate.Name;
            excellence.Logo = excellenceToUpdate.Logo;

            var imageId = excellence.Logo.Split("id=").Last();

            var replaceResult = await _imageRepo.ReplaceFileAsync(imageId,
                excellenceToUpdate.ImageData, excellenceToUpdate.Logo);
            excellenceToUpdate.Logo = replaceResult;

            excellence.Currency = _mapper.Map<Currency>(excellenceToUpdate.Currency);
            excellence.Region = _mapper.Map<Region>(excellenceToUpdate.Region);

            var result = await _excellenceRepo.UpdateEntityAsync(excellence);

            return result;
        }

        public async Task<ContributorExcellenceDto?> GetExcellenceByNameAsync(string name)
        {
            var excellence = await _excellenceRepo
                .GetOneWithNotFoundCheck(x => x.Name == name);

            var result = _mapper.Map<ContributorExcellenceDto>(excellence);
            
            return result;
        }
    }
}
