namespace Configurator.BusinessLogic.Contracts
{
    public interface IDataService
    {
        Task EnsureDatabaseFromFileAsync<T>(string filePath, string collectionName) where T : class;
    }
}
