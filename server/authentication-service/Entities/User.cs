using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_service.Entities
{
    public class User : IdentityUser
    {
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? ville { get; set; }
        public string? date_naissance { get; set; }
    }
}
