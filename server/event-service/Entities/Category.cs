<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace event_service.Entities
{
    public class Category 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public string Name { get; set;}
        [Required]
        public int Seats { get; set;}
        [Required]
        public float Prize { get; set;}
        public string Color { get; set; }
        public int EventId { get; set;}
        public Event Event { get; set;}
    }
}
=======
﻿namespace event_service.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
>>>>>>> authentication
