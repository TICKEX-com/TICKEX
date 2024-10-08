﻿using authentication_service.Entities;

namespace authentication_service.DTOs
{
    public class RegisterReqOrganizerDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string profileImage { get; set; }
        public string currency { get; set; }
        public string PhoneNumber { get; set; }
        public string ville { get; set; }
        public string OrganizationName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Certificate {  get; set; }
    }
}
