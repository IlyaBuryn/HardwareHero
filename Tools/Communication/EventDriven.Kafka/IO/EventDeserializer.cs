using Confluent.Kafka;
using EventDriven.Shared.Events;
using System.Text.Json;

namespace EventDriven.Kafka.IO
{
    public class EventDeserializer<TEvent> : IDeserializer<TEvent> where TEvent : BaseEvent
    {
        public TEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull || data.IsEmpty)
            {
                throw new ArgumentException("Data is null or empty.", nameof(data));
            }

            var result = JsonSerializer.Deserialize<TEvent>(data);

            return result ?? throw new InvalidOperationException("Deserialization resulted in null.");
        }
    }
}
