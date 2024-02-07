using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.Extensions.Options;

namespace HardwareHero.Shared.Repositories.Images
{
    public class ObjectImageRepositoryAsync<T> : IObjectImageRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly string _imagesPath;

        public ObjectImageRepositoryAsync(
            IOptions<ImagesSaveOptions> savePathOptions)
        {
            _imagesPath = savePathOptions.Value.SaveFilePath ?? string.Empty;
        }

        public async Task<string> SaveImageAsync(T entity, byte[] imageData, string fileName)
        {
            var imagePath = Path.Combine(_imagesPath, fileName);
            await File.WriteAllBytesAsync(imagePath, imageData);

            return await Task.FromResult(fileName);
        }

        public async Task<string> DeleteImageAsync(T entity, string fileName)
        {
            var imagePath = Path.Combine(_imagesPath, fileName);
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
                return await Task.FromResult(fileName);
            }
            else
            {
                throw new NotFoundException(fileName);
            }
        }
    }
}
