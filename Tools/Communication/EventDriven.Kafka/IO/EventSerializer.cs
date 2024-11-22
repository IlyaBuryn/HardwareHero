using Confluent.Kafka;
using EventDriven.Shared.Events;
using System.Text.Json;

namespace EventDriven.Kafka.IO
{
    public class EventSerializer<TEvent> : ISerializer<TEvent> where TEvent : BaseEvent
    {
        public byte[] Serialize(TEvent data, SerializationContext context)
        {
            if (data == null) return Array.Empty<byte>();

            return JsonSerializer.SerializeToUtf8Bytes(data);
        }
    }
}
