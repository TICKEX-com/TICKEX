using authentication_service.Data;
using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Services.IServices;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace authentication_service.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(DataContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(HttpContext httpContext, LoginRequestDto requestDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == requestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null };
            }

            //if user was found , Generate JWT Token and add roles 
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                ID = user.Id,
                Username = requestDto.UserName,
                Email = user.Email,
                firstname = user.firstname,
                lastname = user.lastname,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault()
            };

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7), 
            };

            httpContext.Response.Cookies.Append("jwtToken", token, cookieOptions);

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto
            };

            return loginResponseDto;
        }

        // Register a Client
        public async Task<string> RegisterClient(RegisterReqClientDto requestDto)
        {
            // Vérification de l'adresse e-mail
            if (!IsValidEmail(requestDto.Email))
            {
                return "Invalid email format.";
            }

            // Vérification du mot de passe et du mot de passe de confirmation
            if (requestDto.Password != requestDto.ConfirmPassword)
            {
                return "Passwords do not match.";
            }

            // Create user object with the fields we want
            User user = new()
            {
                UserName = requestDto.Username,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                firstname = requestDto.firstname,
                lastname = requestDto.lastname,
                PhoneNumber = requestDto.PhoneNumber
            };
            try
            {
                // Insert user in the database
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var roleName = "CLIENT";
                    if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        //create role if it does not exist
                        _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                    // Add role to user
                    await _userManager.AddToRoleAsync(user, roleName);

                    return "success";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error Encountered";
        }

        // Register an Organizer
        public async Task<string> RegisterOrganizer(RegisterReqOrganizerDto requestDto)
        {
            // Vérification de l'adresse e-mail
            if (!IsValidEmail(requestDto.Email))
            {
                return "Invalid email format.";
            }

            // Vérification du mot de passe et du mot de passe de confirmation
            if (requestDto.Password != requestDto.ConfirmPassword)
            {
                return "Passwords do not match.";
            }

            // Create user object with the fields we want
            User user = new()
            {
                UserName = requestDto.Username,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                firstname = requestDto.firstname,
                lastname = requestDto.lastname,
                PhoneNumber = requestDto.PhoneNumber,
                certificat = requestDto.Certificat
            };
            try
            {
                // Insert user in the database
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var roleName = "ORGANIZER";
                    if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        //create role if it does not exist
                        _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                    // Add role to user
                    await _userManager.AddToRoleAsync(user, roleName);
                    
                    return "success";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error Encountered";
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && email.EndsWith("@gmail.com");
            }
            catch
            {
                return false;
            }
        }
    }
}
