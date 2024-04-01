using authentication_service.DTOs;

namespace authentication_service.Services.IServices
{
    public interface IProducerService
    {
        public Task<bool> publish(string topic, UserDto userdto);
    }
}
