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
                    await producer.ProduceAsync(topic, new Message<Null, OrganizerDto> { Value = organizerDto });
                    producer.Flush(TimeSpan.FromSeconds(10));
                    return true;
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
