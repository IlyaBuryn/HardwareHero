using HardwareHero.Services.Shared.DTOs.Aggregator;

namespace Aggregator.BusinessLogic.Contracts
{
    public interface IComponentImagesService
    {
        Task<Guid?> AddComponentImageAsync(ComponentImagesDto componentImageToAdd);
        Task<bool> RemoveComponentImageAsync(Guid componentImageId);

    }
}
