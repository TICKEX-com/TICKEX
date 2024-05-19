using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_service.Entities
{
    public class User : IdentityUser
    {
        public string? firstname { get; set; } = string.Empty;
        public string? lastname { get; set; } = string.Empty;
        public string? profileImage {  get; set; } = string.Empty;
        public string? currency {  get; set; } = "MAD";
        public string? ville { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string? date_naissance { get; set; } = string.Empty;
        public string? certificate { get; set; } = string.Empty;
        public bool isActive { get; set; } = false;
    }
}
