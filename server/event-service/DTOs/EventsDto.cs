using event_service.Entities;

namespace event_service.DTOs
{
    public class EventsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public float MinPrize { get; set; }
        public string Poster { get; set; }
        public Category Category { get; set; }
    }
}
