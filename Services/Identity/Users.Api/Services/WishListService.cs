using Identity.Shared.Domain;
using Users.Api.Contracts;

namespace Users.Api.Services
{
    public class WishListService : IWishListService
    {
        private readonly UsersDbContext _usersDbContext;

        public WishListService(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }


        public async Task<int> ChangeWishListAsync(string userId, WishListComponent[] components)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            await _usersDbContext.WishListComponents.AddRangeAsync(components);
            var result = await _usersDbContext.SaveChangesAsync();

            return result;
        }


        public async Task<int> ClearWishListAsync(string userId)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var components = await _usersDbContext.WishListComponents.Where(x => x.UserId == userId).ToListAsync();
            _usersDbContext.WishListComponents.RemoveRange(components);
            var result = await _usersDbContext.SaveChangesAsync();

            return result;
        }


        public async Task<IQueryable<WishListComponent>> GetWishListComponentsAsync(string userId)
        {
            var user = await _usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            var components = _usersDbContext.WishListComponents.Where(x => x.UserId == userId);

            return components;
        }
    }
}
