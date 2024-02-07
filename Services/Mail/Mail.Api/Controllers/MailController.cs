using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/mail")]
    [Authorize]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]
        public IActionResult SendMessage([FromBody] MailMessageDto message)
        {
            var response = _mailService.SendMessage(message);

            return Ok(response);
        }
    }
}
