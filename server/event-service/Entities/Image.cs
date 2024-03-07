using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace event_service.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        // url of firebase image
        public string url { get; set; }
        [Required]
        // Poster or other images
        public string type { get; set; }
        [Required]
        public Event Event { get; set; }
    }
}
