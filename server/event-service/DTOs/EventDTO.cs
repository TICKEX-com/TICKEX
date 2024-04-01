namespace event_service.DTOs
{
    public class EventDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public float MinPrize { get; set; }
        
    }
}
