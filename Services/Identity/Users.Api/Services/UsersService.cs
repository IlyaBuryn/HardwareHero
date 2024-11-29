using EventDriven.Shared.Services;
using HardwareHero.Shared.Responses;
using Identity.Shared.Domain;
using Storage.DTOs.Events;
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
        private readonly IRequestService<UploadFileEvent, ReturnFileUrlEvent> _uploadImageService;
        private readonly IRequestService<ChangeFileEvent, ReturnFileUrlEvent> _replaceImageService;
        private readonly IRequestService<DeleteFileEvent, DeleteFileResultEvent> _deleteImageService;
        private readonly ILogger<UsersService> _logger;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            UsersDbContext usersDbContext,
            IRequestService<UploadFileEvent, ReturnFileUrlEvent> uploadImageService,
            IRequestService<ChangeFileEvent, ReturnFileUrlEvent> replaceImageService,
            IRequestService<DeleteFileEvent, DeleteFileResultEvent> deleteImageService,
            ILogger<UsersService> logger)
        {
            _userManager = userManager;
            _usersDbContext = usersDbContext;
            _uploadImageService = uploadImageService;
            _replaceImageService = replaceImageService;
            _deleteImageService = deleteImageService;
            _logger = logger;
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
            user.RegistrationDate = DateTime.UtcNow;

            if (model.ImageData != null)
            {
                var response = await _uploadImageService.SendRequestAsync(
                    new UploadFileEvent()
                    {
                        File = model.ImageData,
                        FileName = string.Join('_', model.UserName, user.RegistrationDate.ToString()),
                    });

                user.ImageUrl = response.FileUrl;
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Created a new user: {model.UserName}");

                return user;
            }

            throw new Exception(result.Errors.First().Description);
        }


        public async Task<ApplicationUser> UpdateUserAsync(UpdateUserRequest model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
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
                var response = await _replaceImageService.SendRequestAsync(
                    new ChangeFileEvent()
                    {
                        NewFile = model.ImageData,
                        OldFileName = string.Join('_', model.UserName, user.RegistrationDate.ToString()),
                        NewFileName = string.Join('_', model.UserName, user.RegistrationDate.ToString()),
                    });

                user.ImageUrl = response.FileUrl;
            }

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.Phone;
            user.CurrencyId = model.CurrencyId;
            user.RegionId = model.RegionId;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"The user has been updated: {model.UserName}");

                return user;
            }

            throw new Exception(result.Errors.First().Description);
        }


        public async Task<bool> RemoveUserAsync(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"The user has been deleted: {user.UserName}");
                var response = await _deleteImageService.SendRequestAsync(
                new DeleteFileEvent()
                   {
                       FileName = string.Join('_', user.UserName, user.RegistrationDate.ToString()),
                   });

                return result.Succeeded;
            }

            throw new Exception(result.Errors.First().Description);
        }


        public async Task<PageResponse<UserResponse>> FetchUsersAsync(UsersFilter filter)
        {
            int totalCount = (int)((int)(await _userManager.Users.CountAsync() + filter.PageSize - 1) / filter.PageSize);
            int skip = (int)((filter.PageNumber - 1) * filter.PageSize);
            var users = await _userManager.Users.Skip(skip)
                .Take((int)filter.PageSize)
                .ToListAsync();

            var page = new PageResponse<UserResponse>()
            {
                CurrentPageNumber = filter.PageNumber,
                CurrentPageSize = filter.PageSize,
                TotalPages = (uint)totalCount,
            };

            var list = new List<UserResponse>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                list.Add(new UserResponse(
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


        public async Task<UserResponse> GetUserByIdAsync(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser));
            }

            var roles = await _userManager.GetRolesAsync(user);
            var components = await _usersDbContext.WishListComponents.Where(x => x.UserId == userId).ToListAsync();
            
            return new UserResponse(
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
                components);
        }
    }
}
