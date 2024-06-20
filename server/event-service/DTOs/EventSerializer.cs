using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace event_service.DTOs
{
    public class EventSerializer : ISerializer<PublishDto>
    {
        public byte[] Serialize(PublishDto data, SerializationContext context)
        {
            if (data == null)
                return null;

            // Convertir l'objet Event en chaîne JSON
            string serializedEvent = JsonConvert.SerializeObject(data);

            // Convertir la chaîne JSON en tableau d'octets
            byte[] serializedBytes = Encoding.UTF8.GetBytes(serializedEvent);

            return serializedBytes;
        }
    }
}
