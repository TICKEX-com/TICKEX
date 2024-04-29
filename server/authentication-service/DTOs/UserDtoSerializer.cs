<<<<<<< HEAD
﻿using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace authentication_service.DTOs
{
    public class UserDtoSerializer : ISerializer<OrganizerDto>
    {
        public byte[] Serialize(OrganizerDto data, SerializationContext context)
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
=======
﻿using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace authentication_service.DTOs
{
    public class UserDtoSerializer : ISerializer<UserDto>
    {
        public byte[] Serialize(UserDto data, SerializationContext context)
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
>>>>>>> authentication
