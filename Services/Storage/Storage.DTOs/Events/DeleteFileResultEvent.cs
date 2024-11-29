using EventDriven.Shared.Events;

namespace Storage.DTOs.Events
{
    public class DeleteFileResultEvent : BaseMessage
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
