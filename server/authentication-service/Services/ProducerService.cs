using authentication_service.DTOs;
using authentication_service.Services.IServices;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace authentication_service.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ProducerConfig _config;

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

        public async Task<bool> Publish(string topic, OrganizerDto organizerDto)
        {
            using var producer = new ProducerBuilder<Null, OrganizerDto>(_config)
                .SetValueSerializer(new UserDtoSerializer())
                .Build();

            try
            {
                // Get metadata for the topic to find the number of partitions
                var adminClientConfig = new AdminClientConfig { BootstrapServers = _config.BootstrapServers };
                using var adminClient = new AdminClientBuilder(adminClientConfig).Build();
                var metadata = adminClient.GetMetadata(topic, TimeSpan.FromSeconds(3));
                var partitions = metadata.Topics.First(t => t.Topic == topic).Partitions;

                // Publish the message to each partition
                foreach (var partition in partitions)
                {
                    var produceTask = producer.ProduceAsync(new TopicPartition(topic, new Partition(partition.PartitionId)),
                        new Message<Null, OrganizerDto> { Value = organizerDto });

                    // Wait for the produce task to complete or timeout
                    var completedTask = await Task.WhenAny(produceTask, Task.Delay(TimeSpan.FromSeconds(5)));
                    if (completedTask != produceTask)
                    {
                        Console.WriteLine($"Publish operation to partition {partition.PartitionId} timed out after 5 seconds.");
                        return false;
                    }
                }

                // Flush the producer to ensure all messages are sent
                producer.Flush(TimeSpan.FromSeconds(5));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing to partitions: {ex.Message}");
                return false;
            }
        }
    }
}
