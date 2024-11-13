using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Answers;
using Microsoft.AspNetCore.Http;

namespace HardwareHero.Shared.Repositories.Contracts
{
    public interface IFileRepositoryAsync
    {
        Task<DataAnswer<IEnumerable<string>>> GetFilesAsync(IPaginable filter);
        Task<DataAnswer<string>> UploadFileAsync(IFormFile file, string? fileName);
        Task<DataAnswer<string>> ReplaceFileAsync(string? fileName, IFormFile file, string newFileName);
        Task<DataAnswer<string>> DeleteFileAsync(string? fileName);
    }
}
