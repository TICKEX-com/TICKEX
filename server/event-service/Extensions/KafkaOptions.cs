namespace event_service.Extensions
{
    public class KafkaOptions
    {
        public string BootstrapServers { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
    }
}
