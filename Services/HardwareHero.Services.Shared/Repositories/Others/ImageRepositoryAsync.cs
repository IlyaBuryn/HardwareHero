using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.Extensions.Options;

namespace HardwareHero.Services.Shared.Repositories.Others
{
    public class ImageRepositoryAsync : IImageRepositoryAsync
    {
        private readonly string _imagesPath;

        public ImageRepositoryAsync(
            IOptions<ImagesSaveOptions> savePathOptions)
        {
            _imagesPath = savePathOptions.Value.SaveFilePath ?? string.Empty;
        }

        public async Task<string> SaveImageAsync(byte[] imageData, string fileName, string? filePath)
        {
            try
            {
                if (imageData == null || fileName == null)
                {
                    return string.Empty;
                }

                if (filePath == null)
                {
                    filePath = _imagesPath;
                }

                var imagePath = Path.Combine(filePath, fileName);
                await File.WriteAllBytesAsync(imagePath, imageData);
            }
            catch
            {
                return string.Empty;
            }

            return fileName;
        }

        public string DeleteImage(string fileName, string? filePath)
        {
            try
            {
                if (fileName == null)
                {
                    return string.Empty;
                }

                if (filePath == null)
                {
                    filePath = _imagesPath;
                }

                var imagePath = Path.Combine(filePath, fileName);
                File.Delete(imagePath);
            }
            catch
            {
                return string.Empty;
            }

            return fileName;
        }

        public async Task<bool> UpdateImageAsync(byte[] imageData, string fileName, string? filePath)
        {
            try
            {
                if (imageData == null || fileName == null)
                {
                    return false;
                }

                if (filePath == null)
                {
                    filePath = _imagesPath;
                }

                var imagePath = Path.Combine(filePath, fileName);
                File.Delete(imagePath);
                await File.WriteAllBytesAsync(imagePath, imageData);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
