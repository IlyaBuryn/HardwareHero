namespace HardwareHero.Services.Shared.Repositories.Contracts
{
    public interface IImageRepositoryAsync
    {
        Task<string> SaveImageAsync(byte[] imageData, string fileName, string? filePath);
        string DeleteImage(string fileName, string? filePath);
        Task<bool> UpdateImageAsync(byte[] imageData, string fileName, string? filePath);
    }
}
