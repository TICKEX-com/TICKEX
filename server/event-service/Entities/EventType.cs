using System.ComponentModel.DataAnnotations;

namespace event_service.Entities
{
    public class EventType
    {
        [Key]
        public string Name { get; set; }
    }
}
