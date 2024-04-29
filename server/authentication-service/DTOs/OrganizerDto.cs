namespace authentication_service.DTOs
{
    public class OrganizerDto
    {

        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string profileImage { get; set; }
        public string currency { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganizationName { get; set; }
        public bool isActive { get; set; }
    }
}
