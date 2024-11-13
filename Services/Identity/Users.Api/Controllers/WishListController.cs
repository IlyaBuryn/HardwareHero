using Identity.Shared.Domain;
using Users.Api.Contracts;

namespace Users.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/wishlist")]
    [Authorize(Roles = Roles.User)]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> ChangeWishListAsync([FromRoute] string userId, [FromBody] Guid[] components)
        {
            var componentsObjects = new WishListComponent[components.Length];
            for (int i = 0; i < components.Length; i++)
            {
                componentsObjects[i] = new WishListComponent
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ComponentId = components[i]
                };
            }
            var result = await _wishListService.ChangeWishListAsync(userId, componentsObjects);

            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearWishListAsync([FromRoute] string userId)
        {
            var result = await _wishListService.ClearWishListAsync(userId);

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishListComponentsAsync([FromRoute] string userId)
        {
            var result = await _wishListService.GetWishListComponentsAsync(userId);

            return Ok(result);
        }
    }
}
