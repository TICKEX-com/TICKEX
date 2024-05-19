using System.ComponentModel.DataAnnotations.Schema;

namespace event_service.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public List<Event> Events { get; set; }
    }
}
