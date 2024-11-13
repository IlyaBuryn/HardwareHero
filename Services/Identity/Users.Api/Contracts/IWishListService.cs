using Identity.Shared.Domain;

namespace Users.Api.Contracts
{
    public interface IWishListService
    {
        Task<int> ChangeWishListAsync(string userId, WishListComponent[] components);
        Task<int> ClearWishListAsync(string userId);
        Task<IQueryable<WishListComponent>> GetWishListComponentsAsync(string userId);
    }
}
