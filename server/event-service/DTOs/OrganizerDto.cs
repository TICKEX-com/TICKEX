namespace event_service.DTOs
{
    public class OrganizerDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
    }
}
