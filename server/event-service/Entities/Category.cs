using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_service.Entities
{
    public class Category 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set;}
        public string Name { get; set;}
        public int Seats { get; set; } = 0;
        [Required]
        public float Price { get; set;}
        public string Color { get; set; } = string.Empty;
        public int EventId { get; set;}
        public Event Event { get; set;}
    }
}