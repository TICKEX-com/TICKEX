using authentication_service.Data;
using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authentication_service.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly ResponseDto _responseDto;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper, UserManager<User> userManager) 
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _userManager = userManager;
            _responseDto = new ResponseDto();
        }

        public async Task<UserDto> GetClientByUsername(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            // Récupérer le rôle de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null || roles.FirstOrDefault() == "ORGANIZER")
            {
                return null;
            }

            // Création de l'objet UserDto à partir de l'utilisateur
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                firstname = user.firstname,
                lastname = user.lastname,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault() // Prend le premier rôle trouvé, à adapter selon votre logique
            };

            return userDto;
        }

        public async Task<UserDto> GetOrganizerByUsername(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            // Récupérer le rôle de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null || roles.FirstOrDefault() == "CLIENT")
            {
                return null;
            }

            // Création de l'objet UserDto à partir de l'utilisateur
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                firstname = user.firstname,
                lastname = user.lastname,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault(), // Prend le premier rôle trouvé, à adapter selon votre logique
                certificat = user.certificat
            };

            return userDto;
        }
    }
}
