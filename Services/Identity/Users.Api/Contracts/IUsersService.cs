using static Users.Api.Records.RequestModels;
using static Users.Api.Records.ResponseModels;

namespace Users.Api.Contracts
{
    public interface IUsersService
    {
        Task<ApplicationUser> CreateUserAsync(CreateUserRequestModel model);
        Task<ApplicationUser> UpdateUserAsync(string userId, UpdateUserRequestModel model);
        Task<bool> RemoveUserAsync(string userId);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<GetUserResponseModel> GetUserByIdAsync(string userId);
    }
}
