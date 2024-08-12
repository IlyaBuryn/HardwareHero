using Identity.Shared.Contexts;
using Users.Api.Contracts;
using Users.Api.Records;
using static Users.Api.Records.ResponseModels;

namespace Users.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UsersDbContext _usersDbContext;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            UsersDbContext usersDbContext)
        {
            _userManager = userManager;
            _usersDbContext = usersDbContext;
        }

        public async Task<ApplicationUser> CreateUserAsync(RequestModels.CreateUserRequestModel model)
        {
            var userNameExist = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExist != null)
            {
                throw new AlreadyExistException<ApplicationUser>(model.UserName);
            }

            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                throw new AlreadyExistException<ApplicationUser>(model.Email);
            }

            var user = new ApplicationUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;
            user.RegistrationDate = DateTime.UtcNow;
            user.FullName = model.FullName;
            user.Image = model.Image;
            
            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded ? user : throw new Exception(result.Errors.First().Description);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();

            return result;
        }

        public async Task<GetUserResponseModel> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var components = await _usersDbContext.WishListComponents.Where(x => x.ApplicationUserId == userId).ToListAsync();
            
            return new GetUserResponseModel(
                user.Id,
                user.Email,
                roles,
                user.UserName,
                user.FullName,
                user.PhoneNumber,
                user.RegionId,
                user.CurrencyId,
                user.Image,
                components);
        }

        public async Task<bool> RemoveUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }

        public async Task<ApplicationUser> UpdateUserAsync(string userId, RequestModels.UpdateUserRequestModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            if (model.UserName != null)
            {
                var existUserName = await _userManager.FindByNameAsync(model.UserName);
                if (existUserName != null)
                {
                    throw new AlreadyExistException<ApplicationUser>(model.UserName);
                }
            }

            user.UserName = model.UserName;
            user.FullName = model.FullName;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;
            user.Image = model.Image;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? user : throw new Exception(result.Errors.First().Description);
        }
    }
}
