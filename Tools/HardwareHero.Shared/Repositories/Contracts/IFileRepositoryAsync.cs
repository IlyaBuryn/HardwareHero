using HardwareHero.Filter.Operations;
using Microsoft.AspNetCore.Http;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface IFileRepositoryAsync
    {
        Task<IList<string>> GetFilesAsync(IPaginable filter);
        Task<string?> UploadFileAsync(IFormFile file, string fileName);
        Task<string?> ReplaceFileAsync(string fileName, IFormFile file, string newFileName);
        Task<bool> DeleteFileAsync(string fileName);
    }
}
