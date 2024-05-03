using event_service.Entities;

namespace event_service.DTOs
{
    public class EventReqDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public float Duration { get; set; }
        public string EventDate { get; set; }
        public string Time { get; set; }
        public string EventType { get; set; }
        public string Poster { get; set; }
        public int DesignId { get; set; }
        public List<CategoryDto> Categories { get; set; }
        // public List<ImageDto>? Images { get; set; } = null;
    }
}
