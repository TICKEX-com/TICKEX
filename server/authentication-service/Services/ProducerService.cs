using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Services.IServices;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

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
            try
            {
                // string serializedEvent = JsonConvert.SerializeObject(e);
                using (var producer = new ProducerBuilder<Null, OrganizerDto>(_config)
                .SetValueSerializer(new UserDtoSerializer())
                .Build())
                {
                    await producer.ProduceAsync(topic, new Message<Null, OrganizerDto> { Value = organizerDto });
                    producer.Flush(TimeSpan.FromSeconds(10));
                    return true;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
}
