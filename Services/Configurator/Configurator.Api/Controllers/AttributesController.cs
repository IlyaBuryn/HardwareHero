using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Configurator.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Produces("application/json")]
    [Route("api/attributes")]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        public AttributesController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet("{type}")]
        [AllowAnonymous]
        public IActionResult GetAttributeGropus([FromRoute] string type)
        {
            var result = _attributeService.GetAttributeGroups(type);

            return Ok(result);
        }
    }
}
