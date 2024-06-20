using event_service.DTOs;
using event_service.Services.IServices;
using Confluent.Kafka;

namespace event_service.Services
{
    public class ProducerService : IProducerService
    {
        private ProducerConfig _config;
        public ProducerService(ProducerConfig config)
        {
            _config = config;
        }
        public async Task<bool> TestKafkaConnectionAsync()
        {
            try
            {
                var config = new AdminClientConfig { BootstrapServers = _config.BootstrapServers };
                using var adminClient = new AdminClientBuilder(config).Build();
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(1));
                return metadata.Brokers.Any();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to Kafka: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> publish(string topic, PublishDto ev)
        {

            // string serializedEvent = JsonConvert.SerializeObject(e);
            using (var producer = new ProducerBuilder<Null, PublishDto>(_config)
            .SetValueSerializer(new EventSerializer())
            .Build())
            {
                try
                {
                    var produceTask = producer.ProduceAsync(topic, new Message<Null, PublishDto> { Value = ev });

                    // Wait for either the produce task to complete or 10 seconds timeout
                    var completedTask = await Task.WhenAny(produceTask, Task.Delay(TimeSpan.FromSeconds(5)));

                    if (completedTask == produceTask)
                    {
                        producer.Flush(TimeSpan.FromSeconds(5));
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Publish operation timed out after 5 seconds.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
