using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace event_service.Extensions
{
    public class Consumer : BackgroundService
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly ILogger<Consumer> _logger;
        private IConsumer<Ignore, string> _consumer;

        public Consumer(IConfiguration configuration, ILogger<Consumer> logger, ConsumerConfig consumerConfig)
        {
            _logger = logger;
            _consumerConfig = consumerConfig;
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe("Tickex");

            stoppingToken.Register(() => _consumer.Close());  // Ensure the consumer is closed properly when the service stops

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = _consumer.Consume(stoppingToken);
                        if (cr != null)
                        {
                            _logger.LogInformation($"Consumed event from Tickex: key = {cr.Message.Key,-10}, value = {cr.Message.Value}");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Consumer error occurred: {e.Error.Reason}");
                    }
                }
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

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer Service is stopping.");
            _consumer.Close();  // Ensure the consumer is closed properly
            await base.StopAsync(cancellationToken);
        }
    }
}