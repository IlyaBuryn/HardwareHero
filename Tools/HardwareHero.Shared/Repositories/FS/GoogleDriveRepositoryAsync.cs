using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using HardwareHero.Filter.Operations;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HardwareHero.Shared.Repositories.FS
{
    public class GoogleDriveRepositoryAsync : IFileRepositoryAsync
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoogleDriveRepositoryAsync> _logger;
        private readonly DriveService _driveService;

        public GoogleDriveRepositoryAsync(
            IConfiguration configuration,
            ILogger<GoogleDriveRepositoryAsync> logger)
        {
            _configuration = configuration;
            _logger = logger;

            var serviceAccountFilePath = _configuration["Google:ServiceAccountFilePath"];
            var applicationName = _configuration["Google:ApplicationName"];

            var credential = GoogleCredential.FromFile(serviceAccountFilePath).CreateScoped(DriveService.ScopeConstants.Drive);

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        public async Task<string?> UploadFileAsync(IFormFile file, string fileName)
        {
            try
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName,
                    Parents = new List<string> { } // Optional: specify a parent folder ID
                };

                FilesResource.CreateMediaUpload request;

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
                    request.Fields = "id";
                    await request.UploadAsync();
                }

                var fileResponse = request.ResponseBody;

                // Set file permissions to make it viewable by anyone with the link
                var permission = new Google.Apis.Drive.v3.Data.Permission()
                {
                    Type = "anyone",
                    Role = "reader"
                };
                await _driveService.Permissions.Create(permission, fileResponse.Id).ExecuteAsync();

                // Return direct link
                return $"https://drive.google.com/thumbnail?id={fileResponse.Id}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the file to Google Drive.");
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            try
            {
                var request = _driveService.Files.Delete(fileName);
                await request.ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the file from Google Drive.");
                return false;
            }
        }

        public async Task<IList<string>> GetFilesAsync(IPaginable filter)
        {
            try
            {
                var request = _driveService.Files.List();
                request.PageSize = (int?)filter.PageSize;
                request.Fields = "nextPageToken, files(id, name)";

                var result = await request.ExecuteAsync();
                return result.Files.Select(item =>
                {
                    return  $"{{" +
                            $"\"id\": \"{item.Id}\"," +
                            $"\"name\": \"{item.Name}\"" +
                            $"}}";
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing files from Google Drive.");
                throw;
            }
        }

        public async Task<string?> ReplaceFileAsync(string fileName, IFormFile file, string newFileName)
        {
            var deleteResult = await DeleteFileAsync(fileName);

            string? result = null;
            if (deleteResult)
            {
                result = await UploadFileAsync(file, newFileName);
            }

            return result;
        }
    }
}