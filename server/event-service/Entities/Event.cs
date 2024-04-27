using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace event_service.Entities
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        public float Duration { get; set; }
        [NotMapped]
        public Design? Design { get; set; } = null;
        public int? DesignId { get; set; } = null;
        [Required]
        public string EventType { get; set; }
        [NotMapped]
        public List<Client> Clients { get; set; }
        [Required]
        public Organizer Organizer { get; set; }
        [Required]
        public string OrganizerId { get; set; }
        [Required]
        public string Poster { get; set; }
        public List<Image> Images { get; set; }
        [Required]
        public List<Category> Categories { get; set; }
        [Required]
        public bool On_sell { get; set; } = true ;
        [Required]
        public bool Is_finished { get; set; } = false ;
    }
}
