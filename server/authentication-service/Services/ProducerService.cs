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
        public async Task<bool> publish(string topic, UserDto userdto)
        {
            UserDto userDto = new()
            {
                Id = userdto.Id,
                Username = userdto.Username,
                Email = userdto.Email,
                firstname = userdto.firstname,
                lastname = userdto.lastname,
                PhoneNumber = userdto.PhoneNumber,
                Role = userdto.Role,
                certificat = userdto.certificat
            };

            // string serializedEvent = JsonConvert.SerializeObject(e);
            using (var producer = new ProducerBuilder<Null, UserDto>(_config)
            .SetValueSerializer(new UserDtoSerializer())
            .Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, UserDto> { Value = userDto });
                producer.Flush(TimeSpan.FromSeconds(10));
                return true;
            }
        }
    }
}
