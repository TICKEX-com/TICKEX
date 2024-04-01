using event_service.Entities;

namespace event_service.DTOs
{
    public class EventReqDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public float MinPrize { get; set; }
        public string OrganizerUsername { get; set; }
        public int CategoryId { get; set; }
        // public IFormFile Poster { get; set; }
        // public IFormFileCollection Images { get; set; }

    }
}
