using Confluent.Kafka;
using EventDriven.Shared.Events;
using System.Text.Json;

namespace EventDriven.Kafka.IO
{
    public class EventDeserializer<TEvent> : IDeserializer<TEvent> where TEvent : BaseEvent
    {
        public TEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull || data.IsEmpty) return null;
            return JsonSerializer.Deserialize<TEvent>(data);
        }
    }
}
