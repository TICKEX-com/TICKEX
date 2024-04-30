using event_service.Entities;

namespace event_service.DTOs
{
    public class EventByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public string Poster { get; set; }
        public float Duration { get; set; }
        public string EventType { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public OrganizerDto Organizer { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
