using EventDriven.Shared.Events;

namespace Storage.DTOs.Events
{
    public class DeleteFileEvent : BaseMessage
    {
        public string? FileName { get; set; }
    }
}
