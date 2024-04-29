using authentication_service.DTOs;
using authentication_service.Services.IServices;
using Confluent.Kafka;

namespace authentication_service.Services
{
    public class ProducerService : IProducerService
    {
        private ProducerConfig _config;
        public ProducerService(ProducerConfig config)
        {
            _config = config;
        }
        public async Task<bool> publish(string topic, OrganizerDto organizerDto)
        {

            // string serializedEvent = JsonConvert.SerializeObject(e);
            using (var producer = new ProducerBuilder<Null, OrganizerDto>(_config)
            .SetValueSerializer(new UserDtoSerializer())
            .Build())
            {
                try
                {
                    var produceTask = producer.ProduceAsync(topic, new Message<Null, OrganizerDto> { Value = organizerDto });

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
