using HardwareHero.Services.Shared.Clients.IdentityServer;
using HardwareHero.Services.Shared.Clients.UserManagementService;
using HardwareHero.Services.Shared.Models.Identity;
using HardwareHero.Services.Shared.Models.UserManagementService;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Requests;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ConsoleTestApp
{
    public class AuthenticationServiceTest
    {
        private readonly IdentityServerApiOptions _identityServerOptions;
        private readonly UserClient _userClient;
        private readonly RoleClient _roleClient;
        private readonly IdentityServerClient _identityServerClient;


        public AuthenticationServiceTest(
            IOptions<IdentityServerApiOptions> options, 
            UserClient userClient, 
            RoleClient roleClient, 
            IdentityServerClient identityServerClient)
        {
            _identityServerOptions = options.Value;
            _userClient = userClient;
            _roleClient = roleClient;
            _identityServerClient = identityServerClient;
        }

        public async Task<string> RunUsersClientTestsAsync(string[] args)
        {
            var token = await _identityServerClient.GetApiTokenAsync(_identityServerOptions);
            _userClient.HttpClient.SetBearerToken(token.AccessToken);

            var userName = "Ilya";
            var roleName = "User";
            var roleNames = new[] { "User", "Contributor" };
             
            var addResult = await _userClient.AddAsync(new CreateUserRequest()
            {
                User = new ApplicationUser()
                {
                    UserName = userName,
                    Name = userName,
                },
                Password = "Password_1",
            });
            Console.WriteLine($"ADD USER: {addResult.Succeeded}");

            Thread.Sleep(100);

            var changePasswordRequest = await _userClient.ChangePasswordAsync(new UserPasswordChangeRequest()
            {
                UserName = userName,
                OldPassword = "Password_1",
                NewPassword = "Password_2",
            });
            Console.WriteLine($"CHANGE PASSWORD USER: {changePasswordRequest.Succeeded}");

            Thread.Sleep(100);

            var getOneRequest = await _userClient.GetAsync(userName);
            Console.WriteLine($"GET ONE USER: {getOneRequest.Code}");

            Thread.Sleep(100);

            var userToUpdate = getOneRequest.Payload;
            userToUpdate.WishList = new WishList()
            {
                Components = new List<WishListComponents>()
                {
                    new WishListComponents() { ComponentId = new Guid("17BB6742-6611-4865-99F4-222610FB1B88") },
                    new WishListComponents() { ComponentId = new Guid("0712D311-71E5-4C5B-8F80-1B1B08180851") },
                }
            };
            userToUpdate.Name = "Ilya";
            userToUpdate.Email = "ilya.b.b@gmail.com";
            var updateResult = await _userClient.UpdateAsync(userToUpdate);
            Console.WriteLine($"UPDATE USER: {updateResult.Succeeded}");

            Thread.Sleep(100);

            var addToRoleRequest = await _userClient.AddToRoleAsync(new AddRemoveRoleRequest()
            {
                UserName = userName,
                RoleName = roleName,
            });
            Console.WriteLine($"ADD TO ROLE USER: {addToRoleRequest.Succeeded}");

            Thread.Sleep(100);

            var removeFromRoleRequest = await _userClient.RemoveFromRoleAsync(new AddRemoveRoleRequest()
            {
                UserName = userName,
                RoleName = roleName,
            });
            Console.WriteLine($"REMOVE FROM ROLE USER: {removeFromRoleRequest.Succeeded}");

            Thread.Sleep(100);

            var addToReolesRequest = await _userClient.AddToRolesAsync(new AddRemoveRolesRequest()
            {
                UserName = userName,
                RoleNames = roleNames,
            });
            Console.WriteLine($"ADD TO MANY ROLES USER: {addToReolesRequest.Succeeded}");

            Thread.Sleep(100);

            var removeFromRolesRequest = await _userClient.RemoveFromRolesAsync(new AddRemoveRolesRequest()
            {
                UserName = userName,
                RoleNames = roleNames,
            });
            Console.WriteLine($"REMOVE FROM MANY ROLES USER: {removeFromRolesRequest.Succeeded}");

            Thread.Sleep(100);

            getOneRequest = await _userClient.GetAsync(userName);
            Console.WriteLine($"GET ONE USER: {getOneRequest.Code}");

            Thread.Sleep(100);

            var deleteResult = await _userClient.RemoveAsync(userName);
            Console.WriteLine($"DELETE USER: {deleteResult.Succeeded}");

            Thread.Sleep(100); 

            var getAllRequest = await _userClient.GetAllAsync();
            Console.WriteLine($"GET ALL USER: {getOneRequest.Code}");

            return "OK";
        }

        public async Task<string> RunRolesClientTestsAsync(string[] args)
        {
            var token = await _identityServerClient.GetApiTokenAsync(_identityServerOptions);
            _roleClient.HttpClient.SetBearerToken(token.AccessToken);

            var roleName = "testRole"; 

            var addResult = await _roleClient.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole(roleName));
            Console.WriteLine($"ADD ROLE: {addResult.Succeeded}");

            Thread.Sleep(100);

            var getOneRequest = await _roleClient.GetAsync(roleName);
            Console.WriteLine($"GET ONE ROLE: {getOneRequest.Code}");

            Thread.Sleep(100);

            var roleToUpdate = getOneRequest.Payload;
            roleToUpdate.Name = "testRoleUpdated";
            var updateRequest = await _roleClient.UpdateAsync(roleToUpdate);
            Console.WriteLine($"UPDATE ROLE: {updateRequest.Succeeded}");

            Thread.Sleep(100);

            getOneRequest = await _roleClient.GetAsync(roleToUpdate.Name);
            Console.WriteLine($"GET ONE ROLE: {getOneRequest.Code}");

            Thread.Sleep(100);

            var deleteResult = await _roleClient.RemoveAsync(roleToUpdate.Name);
            Console.WriteLine($"DELETE ROLE: {deleteResult.Succeeded}");

            Thread.Sleep(100);

            var getAllRequest = await _roleClient.GetAllAsync();
            Console.WriteLine($"GET ALL ROLE: {getAllRequest.Code}");

            return "OK";
        }
    }
}
