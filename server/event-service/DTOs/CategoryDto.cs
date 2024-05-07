using event_service.Entities;

namespace event_service.DTOs
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public float Price { get; set; }
        public string Color { get; set; }
    }
}
