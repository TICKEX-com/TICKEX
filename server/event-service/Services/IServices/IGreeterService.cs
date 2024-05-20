using event_service.Protos;

namespace event_service.Services.IServices
{
    public interface IGreeterService
    {
        Task<string> Hello();

    }
}
