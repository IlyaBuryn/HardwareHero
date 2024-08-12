namespace Users.Api.Contracts
{
    public interface IWishListService
    {
        Task<int> ChangeWishListAsync(string userId, WishListComponents[] components);
        Task<int> ClearWishListAsync(string userId);
        Task<IQueryable<WishListComponents>> GetWishListComponentsAsync(string userId);
    }
}
