using Confluent.Kafka;
using event_service.Entities;
using event_service.Services.IServices;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace event_service.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private IConsumer<Ignore, string> _consumer;
        private readonly IServiceProvider _services;

        public ConsumerService(IServiceProvider services, ILogger<ConsumerService> logger, ConsumerConfig consumerConfig)
        {
            _services = services;
            _logger = logger;
            _consumerConfig = consumerConfig;
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .Build();
        }

        private async Task<bool> TopicExists(string topicName)
        {
            try
            {
                using var adminClient = new AdminClientBuilder(_consumerConfig).Build();
                var metadata = adminClient.GetMetadata(topicName, TimeSpan.FromSeconds(1));
                return metadata.Topics.Any(t => t.Topic == topicName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error checking topic existence: {ex.Message}");
                return false;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer Service running.");

            var topicName = "users";
            while (!stoppingToken.IsCancellationRequested)
            {
                if (await TopicExists(topicName))
                {
                    await DoWork(stoppingToken);
                    break; // Exit the loop once the topic is found and consumption begins
                }
                else
                {
                    _logger.LogInformation($"Kafka topic '{topicName}' does not exist. Waiting for it to be created...");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Wait for some time before checking again
                }
            }
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tickex Consumer is working.");
            using (var scope = _services.CreateScope())
            {
                var user = scope.ServiceProvider.GetRequiredService<IUserService>();

                try
                {
                    // Assign the consumer to partition 0 of the topic 'users'
                    _consumer.Assign(new TopicPartition("users", new Partition(0)));

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var cr = await Task.Run(() => _consumer.Consume(stoppingToken), stoppingToken);
                        if (cr != null)
                        {
                            _logger.LogInformation($"{cr.Message.Value}");

                            // Deserialize JSON to Organizer object
                            var organizer = JsonConvert.DeserializeObject<Organizer>(cr.Message.Value);

                            // Check if organizer already exists
                            try
                            {
                                if (organizer != null && !await user.IsOrganizerExists(organizer.Id))
                                {
                                    // Create organizer if it doesn't exist
                                    if (await user.CreateOrganizer(organizer))
                                    {
                                        _logger.LogInformation("Organizer created successfully.");
                                    }
                                    else
                                    {
                                        _logger.LogError("Failed to create organizer.");
                                    }
                                }
                                else
                                {
                                    // Update organizer if it exists
                                    if (organizer != null && await user.UpdateOrganizer(organizer))
                                    {
                                        _logger.LogInformation("Organizer updated successfully.");
                                    }
                                    else
                                    {
                                        _logger.LogError("Failed to update organizer.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"Error while checking organizer existence: {ex.Message}");
                            }
                        }

                        await Task.Delay(1000, stoppingToken);
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Consumer error occurred: {e.Error.Reason}");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consuming cancelled via cancellation token.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Unexpected error occurred while consuming: {ex.Message}");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tickex Consumer is stopping.");
            _consumer.Close();  // Ensure the consumer is closed properly
            await base.StopAsync(stoppingToken);
        }
    }
}
