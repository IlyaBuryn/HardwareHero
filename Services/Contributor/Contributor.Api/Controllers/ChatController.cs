using Contributor.DTOs.Domain.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly PageSizeOptions _pageSizeOptions;

        public ChatController(
            IChatService chatService,
            IOptions<PageSizeOptions> options)
        {
            _chatService = chatService;
            _pageSizeOptions = options.Value;
        }


        [HttpPost]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> CreateAsync([FromBody] ChatRoomDto chatToAdd)
        {
            var response = await _chatService
                .CreateChatRoomAsync(chatToAdd);
            
            return CreatedAtAction(nameof(CreateAsync), response);
        }


        [HttpPut]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAsync([FromBody] ChatRoomDto chatToUpdate)
        {
            var response = await _chatService
                .UpdateChatRoomAsync(chatToUpdate);

            return Ok(response);
        }


        [HttpDelete("{chatRoomId}")]
        [Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .DeleteChatRoomAsync(chatRoomId);

            return Ok(response);
        }


        [HttpGet("{chatRoomId}")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .GetChatByIdAsync(chatRoomId);

            return Ok(response);
        }


        [HttpGet("contributor/{contributorId}")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetAsPageAsync([FromRoute] Guid contributorId, [FromQuery] ChatRoomFilter filter)
        {
            var response = await _chatService
                .GetChatsByContributorIdAsync(contributorId, filter);

            return Ok(response);
        }


        [HttpPost("message")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> SendMessageAsync([FromBody] ChatMessageDto messageToSend)
        {
            var response = await _chatService
                .SendMessageAsync(messageToSend);
            
            return CreatedAtAction(nameof(SendMessageAsync), response);
        }


        [HttpPut("message")]
        [Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> UpdateMessageAsync([FromBody] ChatMessageDto messageToUpdate)
        {
            var response = await _chatService
                .UpdateMessageAsync(messageToUpdate);

            return Ok(response);
        }

        // TODO: What the difference between same method /\ getByChatRoomId() ?
        //[HttpGet("{chatRoomId}")]
        //[Authorize(Roles = Roles.Contributor)]
        //public async Task<IActionResult> GetMessagesAsync([FromRoute] Guid chatRoomId, [FromQuery] MessagesFilter filter)
        //{
        //    var response = await _chatService
        //        .GetMessagesByChatIdAsync(chatRoomId, filter);

        //    return Ok(response);
        //}
    }
}
