namespace EventDriven.Kafka.Config
{
    public class KafkaConfig
    {
        public string? BootstrapServers { get; set; }
        public string? GroupId { get; set; }
        public bool AllowAutoCreateTopics { get; set; } = true;
    }
}
