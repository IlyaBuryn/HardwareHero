using HardwareHero.Services.Shared.Models.UserManagementService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Duende.IdentityServer.Models;
using IdentityModel.Client;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Exceptions;
using System.Text.RegularExpressions;

namespace UserManagement.Api.Controllers
{
    [Route("ids/account")]
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticateController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HttpClient _httpClient;

        public AuthenticateController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            HttpClient httpClient)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpClient = httpClient;
        }

        public record LoginRequestModel(string Username, string Password, string ReturnUrl, bool RememberLogin = false);
        public record SignUpRequestModel(string Username, string FullName, string Password, string Email, string ReturnUrl);
        public record LoginResponseModel(HardwareHero.Services.Shared.IdentityServer.Token Token, string ReturnUrl, string UserId, string UserName, string FullName);

        [HttpPost("sign-in")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel model)
        {
            var accountIsValid = await AccountIsValid(model);

            if (accountIsValid is null)
            {
                throw new AuthenticationException();
            }

            if (IsValidEmail(model.Username))
            {
                model = new LoginRequestModel(accountIsValid.UserName, model.Password, model.ReturnUrl, model.RememberLogin);
            }
            var tokens = await GetTokens(model);
            var result = new LoginResponseModel(
                Token: new HardwareHero.Services.Shared.IdentityServer.Token
                {
                    AccessToken = tokens.AccessToken,
                    ExpiresIn = tokens.ExpiresIn,
                    IssuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    TokenType = tokens.TokenType,
                    Scope = tokens.Scope + await GetRolesScope(accountIsValid),
                },
                ReturnUrl: model.ReturnUrl,
                UserId: accountIsValid.Id,
                UserName: accountIsValid.UserName,
                FullName: accountIsValid.Name);

            return Ok(result);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequestModel model)
        {
            var isUserNameExist = _userManager.FindByNameAsync(model.Username).Result;
            var isEmailExist = _userManager.FindByEmailAsync(model.Email).Result;

            if (isUserNameExist == null && isEmailExist == null)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Name = model.FullName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    RegistrationDate = DateTime.Now,
                };

                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (!result.Succeeded)
                {
                    throw new AuthenticationException(result.Errors.First().Description);
                }
            }
            else
            {
                throw new AuthenticationException("This account already exist!");
            }

            return Ok(await LoginAsync(new LoginRequestModel(
                Username: model.Username,
                Password: model.Password,
                ReturnUrl: model.ReturnUrl,
                RememberLogin: false
            )));
        }

        private async Task<ApplicationUser> AccountIsValid(LoginRequestModel user)
        {
            Microsoft.AspNetCore.Identity.SignInResult result;
            ApplicationUser account;
            if (IsValidEmail(user.Username))
            {
                account = await _userManager.FindByEmailAsync(user.Username);
                result = await _signInManager.PasswordSignInAsync(account.UserName, user.Password, user.RememberLogin, false);
            }
            else
            {
                account = await _userManager.FindByNameAsync(user.Username);
                result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.RememberLogin, false);
            }

            if (result.Succeeded)
            {
                return account;
            }

            return null;
        }

        private async Task<TokenResponse> GetTokens(LoginRequestModel user)
        {
            var tokenRequest = new PasswordTokenRequest()
            {
                Address = IdentityServerWebAppOptions.Address,
                ClientId = IdentityServerWebAppOptions.ClientId,
                Scope = IdentityServerWebAppOptions.Scope,
                UserName = user.Username,
                Password = user.Password,
                ClientSecret = IdentityServerWebAppOptions.ClientSecret.Sha256()
            };

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
            {
                throw new AuthenticationException("Can't take access token!");
            }

            return tokenResponse;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private async Task<string> GetRolesScope(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = string.Empty;
            foreach (var item in roles)
            {
                rolesString += ' ' + item;
            }

            return rolesString;
        }
    }
}
