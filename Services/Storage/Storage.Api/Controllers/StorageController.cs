using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.BusinessLogic.Contracts;

namespace Storage.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IFileEventService _fileEventService;

        public StorageController(
            IFileService fileService, 
            IFileEventService fileEventService)
        {
            _fileService = fileService;
            _fileEventService = fileEventService;
        }


        [HttpPost("file/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFileAsync([FromBody] IFormFile file, [FromRoute] string fileName)
        {
            var response = await _fileEventService.UploadFileAsync(
                new DTOs.Events.UploadFileEvent()
                {
                    File = file,
                    FileName = fileName
                });

            return CreatedAtAction(nameof(UploadFileAsync), response);
        }


        [HttpDelete("file/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteFileAsync([FromRoute] string fileName)
        {
            var response = await _fileEventService.DeleteFileAsync(
                new DTOs.Events.DeleteFileEvent()
                {
                    FileName = fileName
                });

            return Ok(response);
        }
    }
}
