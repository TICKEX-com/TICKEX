using event_service.Entities;

namespace event_service.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Seats { get; set; }
        public float Prize { get; set; }
    }
}
