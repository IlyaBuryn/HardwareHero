using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Contributor.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/chat")]
    [Authorize]
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
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> CreateAsync([FromBody] ChatRoomDto chatToAdd)
        {
            var response = await _chatService
                .CreateChatRoomAsync(chatToAdd);
            
            return CreatedAtAction(nameof(CreateAsync), response);
        }

        [HttpPut]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> UpdateAsync([FromBody] ChatRoomDto chatToUpdate)
        {
            var response = await _chatService
                .UpdateChatRoomAsync(chatToUpdate);

            return Ok(response);
        }

        [HttpDelete("{chatRoomId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Manager)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .DeleteChatRoomAsync(chatRoomId);

            return Ok(response);
        }

        [HttpGet("{chatRoomId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid chatRoomId)
        {
            var response = await _chatService
                .GetChatByIdAsync(chatRoomId);

            return Ok(response);
        }

        [HttpPost("contributor/{contributorId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetAsPageAsync([FromRoute] Guid contributorId, [FromBody] FilterRequestDomain<ChatRoom> filter)
        {
            filter.ApplyPageSizeOptions(_pageSizeOptions);

            var paginationInfo = PaginationInfo.ConvertFromFilterPagination(filter.PageRequestInfo);
            var response = await _chatService
                .GetChatsByContributorIdAsync(contributorId, paginationInfo);

            return Ok(response);
        }


        [HttpPost("message")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> SendMessageAsync([FromBody] ChatMessageDto messageToSend)
        {
            var response = await _chatService
                .SendMessageAsync(messageToSend);
            
            return CreatedAtAction(nameof(SendMessageAsync), response);
        }

        [HttpPut("message")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> UpdateMessageAsync([FromBody] ChatMessageDto messageToUpdate)
        {
            var response = await _chatService
                .UpdateMessageAsync(messageToUpdate);

            return Ok(response);
        }

        [HttpPost("{chatRoomId}")]
        [AllowAnonymous]
        //[Authorize(Roles = Roles.Contributor)]
        public async Task<IActionResult> GetMessagesAsync([FromRoute] Guid chatRoomId, [FromBody] FilterRequestDomain<ChatMessage> filter)
        {
            filter.ApplyPageSizeOptions(_pageSizeOptions);

            var paginationInfo = PaginationInfo.ConvertFromFilterPagination(filter.PageRequestInfo);
            var response = await _chatService
                .GetMessagesByChatIdAsync(chatRoomId, paginationInfo);

            return Ok(response);
        }
    }
}
