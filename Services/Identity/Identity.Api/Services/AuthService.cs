using Identity.Api.Contracts;
using Identity.Api.Data;
using Identity.Api.Records;

namespace Identity.Api.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly GrantsDbContext _grantDbContext;
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParameters,
            GrantsDbContext grantDbContext,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
            _grantDbContext = grantDbContext;
            _logger = logger;
        }


        public async Task<ApplicationUser> SignInAsync(RequestModels.SignInRequestModel model)
        {
            ApplicationUser? existingEmail = null;
            var modelContainsEmail = false;
            var existingUsername = await _userManager.FindByNameAsync(model.UsernameOrEmail);
            if (existingUsername == null)
            {
                existingEmail = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
                if (existingEmail == null)
                {
                    throw new AuthenticationException();
                }
                modelContainsEmail = true;
            }

            var user = modelContainsEmail ? existingEmail! : existingUsername!;
            var isValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isValid)
            {
                await RevokeOldTokensAsync(user.Id);

                return user;
            }

            throw new AuthenticationException();
        }

        public async Task<ApplicationUser> SignUpAsync(RequestModels.SignUpRequestModel model)
        {
            var existingUsername = await _userManager.FindByNameAsync(model.Username);
            if (existingUsername != null)
            {
                throw new AuthenticationException("This user already exist!");
            }

            var existingEmail = _userManager.FindByEmailAsync(model.Email).Result;
            if (existingEmail != null)
            {
                throw new AuthenticationException("This user already exist!");
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                RegistrationDate = DateTime.Now,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new AuthenticationException(result.Errors.First().Description);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, Roles.User);

            if (roleResult.Succeeded)
            {
                return user;
            }

            throw new AuthenticationException(roleResult.Errors.First().Description);
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(RequestModels.TokenRequest tokenRequest)
        {
            var result = await VerifyTokenAsync(tokenRequest);

            if (result == null)
            {
                throw new AuthenticationProblemException("Invalid tokens!");
            }

            return result;
        }

        public async Task<AuthenticationResponse?> VerifyTokenAsync(RequestModels.TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var principal = jwtTokenHandler.ValidateToken(tokenRequest.AccessToken, _tokenValidationParameters, out var validatedToken);

            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                {
                    return null;
                }
            }

            var utcExpiryDate = long.Parse(principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expDate = UnixTimeStampToDateTime(utcExpiryDate);

            if (expDate > DateTime.UtcNow)
            {
                return new AuthenticationResponse
                {
                    IsSuccessful = false,
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty,
                    Errors = new() { "We cannot refresh this since the token has not expired!" }
                };
            }

            var storedRefreshToken = await _grantDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

            if (storedRefreshToken == null)
            {
                throw new AuthenticationProblemException("Refresh token doesn't exist!");
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                throw new AuthenticationProblemException("Token has expired, user needs to relogin!");
            }

            if (storedRefreshToken.IsUsed)
            {
                throw new AuthenticationProblemException("Token has been used!");
            }

            if (storedRefreshToken.IsRevoked)
            {
                throw new AuthenticationProblemException("Token has been revoked!");
            }

            var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            if (storedRefreshToken.JwtId != jti)
            {
                throw new AuthenticationProblemException("The token doesn't matched the saved token!");
            }

            storedRefreshToken.IsUsed = true;
            _grantDbContext.RefreshTokens.Update(storedRefreshToken);
            await _grantDbContext.SaveChangesAsync();

            var dbUser = await _userManager.FindByIdAsync(storedRefreshToken.UserId);

            return await GenerateJwtTokenAsync(dbUser);
        }

        public async Task<AuthenticationResponse> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaimsAsync(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(5),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await _grantDbContext.RefreshTokens.AddAsync(refreshToken);
            await _grantDbContext.SaveChangesAsync();

            return new AuthenticationResponse()
            {
                AccessToken = jwtToken,
                IsSuccessful = true,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<List<Claim>> GetAllValidClaimsAsync(ApplicationUser user) 
        {
            var _option = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("scope", HardwareHero.Shared.Constants.IdentityConstants.ServicesApiScope)
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {                
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task RevokeOldTokensAsync(string userId)
        {
            try
            {
                var tokens = await _grantDbContext.RefreshTokens
                    .Where(rt => rt.UserId == userId)
                    .OrderByDescending(rt => rt.AddedDate)
                    .ToListAsync();

                var tokensToRevoke = tokens.Skip(2).ToList();

                foreach (var token in tokensToRevoke)
                {
                    token.IsRevoked = true;
                }

                await _grantDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Can't revoke one or more tokens!");
                return;
            }
        }
    }
}
