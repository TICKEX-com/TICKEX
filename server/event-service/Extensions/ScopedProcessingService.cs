using Confluent.Kafka;

namespace event_service.Extensions
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private readonly ConsumerConfig _consumerConfig;
        private IConsumer<Ignore, string> _consumer;



        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, ConsumerConfig consumerConfig)
        {
            _logger = logger;
            _consumerConfig = consumerConfig;
            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            try
            {
                _consumer.Subscribe("Tickex");

                stoppingToken.Register(() => _consumer.Close());  // Ensure the consumer is closed properly when the service stops

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

                    await Task.Delay(10000, stoppingToken);
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
            finally
            {
                _consumer.Close();  // Ensure the consumer is closed properly
            }
        }


    }
}

