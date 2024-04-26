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
        public string Location { get; set; }
        [Required]
        public float MinPrize { get; set; }
        [NotMapped]
        public Design? Design { get; set; } = null;
        public int? DesignId { get; set; } = null;
        [Required]
        public EventType EventType { get; set; }
        [Required]
        public int EventTypeId { get; set; }
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
        public bool On_sell { get; set; } = false ;
        [Required]
        public bool Is_finished { get; set; } = false ;
    }
}
