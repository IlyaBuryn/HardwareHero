namespace Storage.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepositoryAsync _fileStorageRepo;

        public FileService(IFileRepositoryAsync fileStorageRepo)
        {
            _fileStorageRepo = fileStorageRepo;
        }


        public Task<IEnumerable<string>> GetFilesAsync()
        {
            // TODO: NotImplementedException
            throw new NotImplementedException();
        }
    }
}
