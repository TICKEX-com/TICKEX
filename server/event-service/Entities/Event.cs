using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public Design design { get; set; }
        [Required]
        public int DesignId { get; set; }
        [NotMapped]
        public List<Client> Clients { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [NotMapped]
        public Organizer Organizer { get; set; }
        [Required]
        public int OrganizerId { get; set; }
        [Required]
        public List<Image> Images { get; set; }
        [Required]
        public bool Is_public { get; set; }
        [Required]
        public bool On_sell { get; set; }
        [Required]
        public bool Is_finished { get; set; }
    }
}
