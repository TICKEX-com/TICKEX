using event_service.DTOs;

namespace event_service.Services.IServices
{
    public interface IProducerService
    {
        public Task<bool> publish(string topic, PublishDto ev);
        public Task<bool> TestKafkaConnectionAsync();

    }
}
