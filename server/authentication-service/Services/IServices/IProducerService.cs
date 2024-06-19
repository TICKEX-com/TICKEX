using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IProducerService
    {
        public Task<bool> Publish(string topic, OrganizerDto organizerDto);
        public Task<bool> TestKafkaConnectionAsync();

    }
}
