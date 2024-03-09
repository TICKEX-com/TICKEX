namespace event_service.DTOs
{
    public class EventByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public float MinPrize { get; set; }
        
    }
}
