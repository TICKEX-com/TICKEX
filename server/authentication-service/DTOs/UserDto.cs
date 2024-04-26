using authentication_service.Entities;
using System.Runtime.ConstrainedExecution;

namespace authentication_service.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Role { get; set; } = string.Empty;
        public string? certificat {  get; set; } = string.Empty;
    }
}
