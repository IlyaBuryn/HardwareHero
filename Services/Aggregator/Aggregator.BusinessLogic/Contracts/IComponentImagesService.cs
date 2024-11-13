using Aggregator.DTOs.Components;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentImagesService
    {
        Task<Guid?> AddComponentImageAsync(ComponentImageDto componentImageToAdd);
        Task<bool> RemoveComponentImageAsync(Guid componentImageId, bool acceptUnrevoked = false);
        Task<bool> ChangeActiveImageStatusAsync(Guid componentImageId);

    }
}
