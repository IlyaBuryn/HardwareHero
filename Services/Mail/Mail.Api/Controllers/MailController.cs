using Mail.BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("mail")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync()
        {
            return Ok();
        }
    }
}
