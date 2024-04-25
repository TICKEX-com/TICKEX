using authentication_service.Entities;

namespace authentication_service.DTOs
{
    public class UpdateReqOrganizerDto
    {
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
    }
}
