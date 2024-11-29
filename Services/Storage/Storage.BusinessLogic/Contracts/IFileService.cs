namespace Storage.BusinessLogic.Contracts
{
    public interface IFileService
    {
        Task<IEnumerable<string>> GetFilesAsync();
    }
}
