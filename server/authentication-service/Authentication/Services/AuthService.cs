using Authentication.Data;
using Authentication.DTOs;
using Authentication.Entities;
using Authentication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Services
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

        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == requestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //if user was found , Generate JWT Token and add roles 
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                ID = user.Id,
                Email = user.Email,
                firstname = user.firstname,
                lastname = user.lastname,
                PhoneNumber = user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegisterationRequestDto requestDto)
        {
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
                    var userToReturn = _db.Users.First(u => u.Email == requestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        firstname = userToReturn.firstname,
                        lastname = userToReturn.lastname,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
