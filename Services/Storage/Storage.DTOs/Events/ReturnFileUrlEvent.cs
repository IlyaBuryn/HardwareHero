using EventDriven.Shared.Events;

namespace Storage.DTOs.Events
{
    public class ReturnFileUrlEvent : BaseMessage
    {
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
