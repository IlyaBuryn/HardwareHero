using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Responses;
using Identity.Shared.Domain;
using Users.Api.Contracts;
using Users.Api.Filters;
using static Identity.Shared.Requests.IdentityRequestRecords;
using static Identity.Shared.Responses.IdentityResponseRecords;

namespace Users.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UsersDbContext _usersDbContext;
        private readonly IFileRepositoryAsync _fileRepository;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            UsersDbContext usersDbContext,
            IFileRepositoryAsync fileRepository)
        {
            _userManager = userManager;
            _usersDbContext = usersDbContext;
            _fileRepository = fileRepository;
        }

        public async Task<ApplicationUser> CreateUserAsync(CreateUserRequest model)
        {
            var userNameExist = await _userManager.FindByNameAsync(model.UserName);
            if (userNameExist != null)
            {
                throw new AlreadyExistException(model.UserName, typeof(ApplicationUser));
            }

            var emailExist = await _userManager.FindByEmailAsync(model.Email);
            if (emailExist != null)
            {
                throw new AlreadyExistException(model.Email, typeof(ApplicationUser));
            }

            var user = new ApplicationUser();

            if (model.ImageData != null)
            {
                var imageName = $"{model.UserName}_{DateTime.UtcNow}";
                var imageUrl = await _fileRepository.UploadFileAsync(
                    model.ImageData, imageName);
                imageUrl.DataAnswerCheck();

                if (imageUrl.IsSuccess && !string.IsNullOrEmpty(imageUrl.Value))
                {
                    user.ImageUrl = imageUrl.Value;
                    user.ImageName = imageName;
                }
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;
            user.RegistrationDate = DateTime.UtcNow;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            
            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded ? user : throw new Exception(result.Errors.First().Description);
        }

        public async Task<PageResponse<FetchUserResponse>> FetchUsersAsync(UsersFilter filter)
        {
            int totalCount = (int)((int)(await _userManager.Users.CountAsync() + filter.PageSize - 1) / filter.PageSize);
            int skip = (int)((filter.PageNumber - 1) * filter.PageSize);
            var users = await _userManager.Users.Skip(skip)
                .Take((int)filter.PageSize)
                .ToListAsync();

            var page = new PageResponse<FetchUserResponse>()
            {
                CurrentPageNumber = filter.PageNumber,
                CurrentPageSize = filter.PageSize,
                TotalPages = (uint)totalCount,
            };

            var list = new List<FetchUserResponse>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                list.Add(new FetchUserResponse(
                    user.Id,
                    user.Email!,
                    user.UserName!,
                    roles,
                    user.RegistrationDate,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.RegionId,
                    user.CurrencyId,
                    user.ImageUrl,
                    null));
            }
            page.Items = list;

            return page;
        }

        public async Task<FetchUserResponse> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var components = await _usersDbContext.WishListComponents.Where(x => x.UserId == userId).ToListAsync();
            
            return new FetchUserResponse(
                user.Id,
                user.Email,
                user.UserName,
                roles,
                user.RegistrationDate,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.RegionId,
                user.CurrencyId,
                user.ImageUrl,
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
            var imageDelete = await _fileRepository.DeleteFileAsync(user.ImageName);

            return result.Succeeded ? true : throw new Exception(result.Errors.First().Description);
        }

        public async Task<ApplicationUser> UpdateUserAsync(string userId, UpdateUserRequest model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(user));
            }

            if (model.UserName != null && user.UserName != model.UserName)
            {
                var existUserName = await _userManager.FindByNameAsync(model.UserName);
                if (existUserName != null)
                {
                    throw new AlreadyExistException(model.UserName, typeof(ApplicationUser));
                }
            }

            if (model.ImageData != null)
            {
                if (user.ImageName != null && user.ImageUrl != null)
                {
                    var imageDelete = await _fileRepository.DeleteFileAsync(user.ImageName);
                }
                var imageName = $"{user.UserName}_{DateTime.UtcNow}";
                var imageUrl = await _fileRepository.UploadFileAsync(
                    model.ImageData, imageName);
                imageUrl.DataAnswerCheck();

                user.ImageUrl = imageUrl.Value!;
            }

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.Phone;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? user : throw new Exception(result.Errors.First().Description);
        }
    }
}
