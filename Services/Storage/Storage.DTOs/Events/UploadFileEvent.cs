using EventDriven.Shared.Events;
using Microsoft.AspNetCore.Http;

namespace Storage.DTOs.Events
{
    public class UploadFileEvent : BaseMessage
    {
        public string? FileName { get; set; }
        public IFormFile? File { get; set; }
    }
}
