using Confluent.Kafka;
using event_service.Entities;
using event_service.Services.IServices;
using Newtonsoft.Json;

namespace event_service.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private IConsumer<Ignore, string> _consumer;
        public IServiceProvider _services;



        public ConsumerService(IServiceProvider services, ILogger<ConsumerService> logger, ConsumerConfig consumerConfig)
        {
            _services = services;
            _logger = logger;
            _consumerConfig = consumerConfig;
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

        }

        private async Task<bool> TopicExists(string topicName)
        {
            try
            {
                using var adminClient = new AdminClientBuilder(_consumerConfig).Build();
                var metadata = adminClient.GetMetadata(topicName, TimeSpan.FromSeconds(10));
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

            var topicName = "Tickex";
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
            // Inside your DoWork method, you can use the IUserService to create organizers


            _logger.LogInformation(
                "Tickex Consumer is working.");
            using (var scope = _services.CreateScope())
            {
                var user = scope.ServiceProvider.GetRequiredService<IUserService>();

                try
                {
                    _consumer.Subscribe("Tickex");

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
                                if (!await user.IsOrganizerExists(organizer.Id))
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
                                    // Create organizer if it doesn't exist
                                    if (await user.UpdateOrganizer(organizer))
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
                    Console.WriteLine(ex.Message);
                    _logger.LogError($"Unexpected error occurred while consuming: {ex.Message}");
                }

            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Tickex Consumer is stopping.");
            _consumer.Close();  // Ensure the consumer is closed properly
            await base.StopAsync(stoppingToken);
        }
    }
}
