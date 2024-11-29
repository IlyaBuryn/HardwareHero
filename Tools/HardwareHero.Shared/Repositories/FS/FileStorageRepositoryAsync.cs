using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Answers;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HardwareHero.Shared.Repositories.FS
{
    public class FileStorageRepositoryAsync : IFileRepositoryAsync
    {
        private readonly ILogger<FileStorageRepositoryAsync> _logger;
        private readonly string _baseStoragePath;
        private readonly string _baseUrl;

        public FileStorageRepositoryAsync(
            IConfiguration configuration,
            ILogger<FileStorageRepositoryAsync> logger)
        {
            _logger = logger;
            _baseStoragePath = configuration["Storage:BasePath"] ?? "/data/files";
            _baseUrl = configuration["Storage:BaseUrl"] ?? "http://localhost:5000/files";
        }


        public async Task<DataAnswer<string>> UploadFileAsync(IFormFile file, string? fileName)
        {
            try
            {
                var targetFileName = fileName ?? file.FileName;
                var folder = Path.Combine(_baseStoragePath, file.ContentType);
                Directory.CreateDirectory(folder);

                var filePath = Path.Combine(folder, targetFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"{_baseUrl}/{file.ContentType}/{targetFileName}";
                _logger.LogInformation($"Uploaded a new file: {fileUrl}");

                return new (fileUrl);
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }


        public async Task<DataAnswer<string>> DeleteFileAsync(string? fileName)
        {
            try
            {
                var filePath = Path.Combine(_baseStoragePath, fileName ?? "");
                if (!File.Exists(filePath))
                {
                    return new(new NotFoundException(nameof(filePath)));
                }

                File.Delete(filePath);
                _logger.LogInformation($"Deleted a file: {filePath}");

                return new (fileName);
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }


        public async Task<DataAnswer<string>> ReplaceFileAsync(string? fileName, IFormFile file, string newFileName)
        {
            try
            {
                var folder = Path.Combine(_baseStoragePath, file.ContentType);
                var oldFilePath = Path.Combine(folder, fileName ?? "");
                var newFilePath = Path.Combine(folder, newFileName);

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                    _logger.LogInformation($"Deleted a file: {oldFilePath}");
                }

                Directory.CreateDirectory(folder);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var fileUrl = $"{_baseUrl}/{file.ContentType}/{newFileName}";
                _logger.LogInformation($"Uploaded a new file: {fileUrl}");

                return new (fileUrl);
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }

        
        // TODO: 💀
        public async Task<DataAnswer<IEnumerable<string>>> GetFilesAsync(IPaginable filter)
        {
            try
            {
                if (!Directory.Exists(_baseStoragePath))
                {
                    return new (Enumerable.Empty<string>());
                }

                var files = Directory.GetFiles(_baseStoragePath)
                                      .Skip((int)(filter.PageNumber * filter.PageSize))
                                      .Take((int)filter.PageSize)
                                      .Select(f => Path.GetFileName(f));

                return new (files);
            }
            catch (Exception ex)
            {
                return new (ex);
            }
        }
    }
}
