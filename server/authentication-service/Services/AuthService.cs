using authentication_service.Data;
using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Services.IServices;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Google.Api.Gax.ResourceNames;

namespace authentication_service.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthService(DataContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
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
            try
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == requestDto.UserName.ToLower());
                bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

                if (user == null || isValid == false)
                {
                    return new LoginResponseDto() { User = null };
                }

                //if user was found , Generate JWT Token and add roles 
                var roles = await _userManager.GetRolesAsync(user);
                // var firstRole = roles.FirstOrDefault(); // Get the first role
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                OrganizerDto organizerDto = new()
                {
                    Id = user.Id,
                    Username = requestDto.UserName,
                    Email = user.Email,
                    firstname = user.firstname,
                    lastname = user.lastname,
                    PhoneNumber = user.PhoneNumber,
                    profileImage = user.profileImage,
                    currency = user.currency,
                    OrganizationName = user.OrganizationName,
                    ville = user.ville,
                    isActive = user.isActive,
                    certificate = user.certificate,
                    Role = roles.FirstOrDefault()
                };

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Expires = DateTime.UtcNow.AddHours(3),
                };

                httpContext.Response.Cookies.Append("jwtToken", token, cookieOptions);

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = organizerDto
                };

                return loginResponseDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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

            // Vérification si l'e-mail existe déjà dans la base de données
            var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);
            if (existingUser != null)
            {
                return "Email already exists.";
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
                profileImage = requestDto.profileImage
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
            catch (DbUpdateException ex)
            {
                return "An error occurred while saving the entity changes. Database update exception: " + ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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

            // Vérification si l'e-mail existe déjà dans la base de données
            var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);
            if (existingUser != null)
            {
                return "Email already exists.";
            }

            // Create user object with the fields we want
            User user = new()
            {
                UserName = requestDto.Username,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                firstname = requestDto.firstname,
                lastname = requestDto.lastname,
                OrganizationName = requestDto.OrganizationName,
                PhoneNumber = requestDto.PhoneNumber,
                certificate = requestDto.Certificate,
                profileImage = requestDto.profileImage,
                currency = requestDto.currency,
                ville = requestDto.ville
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
        }

        public async Task<bool> Logout(HttpContext httpContext)
        {
            try
            {
                httpContext.Response.Cookies.Delete("jwtToken");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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
