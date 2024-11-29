using EventDriven.Shared.Events;
using Microsoft.AspNetCore.Http;

namespace Storage.DTOs.Events
{
    public class ChangeFileEvent : BaseMessage
    {
        public string? OldFileName { get; set; }
        public string? NewFileName { get; set; }
        public IFormFile? NewFile { get; set; }
    }
}
