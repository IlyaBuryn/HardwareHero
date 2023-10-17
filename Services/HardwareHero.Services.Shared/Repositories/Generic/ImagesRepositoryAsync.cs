using HardwareHero.Services.Shared.Models;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace HardwareHero.Services.Shared.Repositories.Generic
{
    public class ImagesRepositoryAsync<T> : IIMagesRepositoryAsync<T>
        where T : BaseEntity
    {
        private readonly string _imagesPath;
        private readonly string _fileNameDivider;

        public ImagesRepositoryAsync(
            IOptions<DataSaveOptions> savePathOptions)
        {
            _imagesPath = savePathOptions.Value.SaveFilePath;
            _fileNameDivider = savePathOptions.Value.FileNameDivider;
        }

        public async Task<string> SaveImageAsync(T entity, byte[] imageData, Expression<Func<T, string>>[] fileNameParts)
        {
            string fileName = GetFileName(entity, fileNameParts);

            var imagePath = Path.Combine(_imagesPath, fileName);
            await File.WriteAllBytesAsync(imagePath, imageData);

            return await Task.FromResult(fileName);
        }

        public async Task<string> DeleteImageAsync(T entity, Expression<Func<T, string>>[] fileNameParts)
        {
            string fileName = GetFileName(entity, fileNameParts);

            var imagePath = Path.Combine(_imagesPath, fileName);
            File.Delete(imagePath);

            return await Task.FromResult(fileName);
        }

        protected string GetFileName(T entity, Expression<Func<T, string>>[] fileNameParts)
        {
            var fieldValues = new List<string>();

            if (fileNameParts != null)
            {
                foreach (var part in fileNameParts)
                {
                    if (part.Body is MemberExpression memberExpression)
                    {
                        var memberName = memberExpression.Member.Name;
                        var compiledExpression = part.Compile();
                        var fieldValue = compiledExpression.Invoke(entity);
                        fieldValues.Add(fieldValue.ToString());
                    }
                    else
                    {
                        throw new ArgumentException("Invalid expression type. Expected MemberExpression.");
                    }
                }
            }

            string fileName = string.Join(_fileNameDivider, fieldValues);

            return fileName;
        }
    }
}
