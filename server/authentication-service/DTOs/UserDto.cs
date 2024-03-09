namespace authentication_service.DTOs
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
