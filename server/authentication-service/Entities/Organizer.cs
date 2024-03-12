using authentication_service.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_service.Entities
{
    public class Organizer : UserDto
    {
        [Required]
        public string? certificat { get; set; }
    }
}
