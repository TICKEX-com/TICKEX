using event_service.Entities;

namespace event_service.DTOs
{
    public class EventsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string City { get; set; }
        public float MinPrice { get; set; }
        public string Poster { get; set; }
        public string EventType { get; set; }
    }
}
