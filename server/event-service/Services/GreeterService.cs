using Azure.Core;
using event_service.Protos;
using event_service.Services.IServices;
using Grpc.Core;

namespace event_service.Services
{
    public class GreeterService : IGreeterService
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly Greeter.GreeterClient _client;

        public GreeterService(ILogger<GreeterService> logger, Greeter.GreeterClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<string> Hello()
        {
            var helloRequest = new HelloRequest
            {
                Name = "Anas Chatt"
            };
            try
            {
                HelloReply response = await _client.SayHelloAsync(helloRequest);
                Console.WriteLine(response.Message);
                return response.Message;
            }
            catch (RpcException e)
            {
                _logger.LogWarning(e, "ERROR - Parameters: {@parameters}", helloRequest);

                return null;
            }
            
        }
    }
}
