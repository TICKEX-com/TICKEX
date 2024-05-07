using authentication_service.Data;
using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
                Role = roles.FirstOrDefault()
            };

            return userDto;
        }

        public async Task<string> GetClientIdByUsername(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            // Récupérer le rôle de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null || roles.FirstOrDefault() == "ORGANIZER")
            {
                return null;
            }

            return user.Id;
        }

        public async Task<ICollection<OrganizerDto>> GetOrganizers()
        {
            // Get all users
            var users = await _dataContext.Users.ToListAsync();

            // Filter users to only include those with the role "ORGANIZER"
            var organizerUsers = users.Where(u => _userManager.IsInRoleAsync(u, "ORGANIZER").Result).ToList();

            // Map filtered users to UserDto
            var organizerDtos = _mapper.Map<ICollection<OrganizerDto>>(organizerUsers);

            return organizerDtos;
        }

        public async Task<OrganizerDto> GetOrganizerById(string Id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user != null)
            {
                // Récupérer le rôle de l'utilisateur
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.FirstOrDefault() == "CLIENT" || roles.FirstOrDefault() == "ADMIN")
                {
                    return null;
                }
                // Création de l'objet UserDto à partir de l'utilisateur
                var organizerDto = _mapper.Map<OrganizerDto>(user);
                organizerDto.Role = "ORGANIZER";
                return organizerDto;
            }
            return null;
        }

        public async Task<User> GetOrganizerById2(string id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                // Récupérer le rôle de l'utilisateur
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.FirstOrDefault() == "CLIENT")
                {
                    return null;
                }
            }
            return user;
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
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<string> GetOrganizerIdByUsername(string username)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            // Récupérer le rôle de l'utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            if (user == null || roles.FirstOrDefault() == "CLIENT")
            {
                return null;
            }

            return user.Id;
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

        public async Task<string> UpdateOrganizer(UpdateReqOrganizerDto requestDto, string id)
        {
            var existingOrganizer = await GetOrganizerById2(id);
            if (existingOrganizer == null)
            {
                return "Organizer not found.";
            }

            // Vérification de l'adresse e-mail
            if (!IsValidEmail(requestDto.Email))
            {
                return "Invalid email format.";
            }


            // Check if the email already exists in the database (excluding the current organizer)
            var existingUserWithEmail = await _userManager.FindByEmailAsync(requestDto.Email);
            if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
            {
                return "Email already exists.";
            }

            // Create user object with the fields we want

            existingOrganizer.Email = requestDto.Email;
            existingOrganizer.firstname = requestDto.firstname;
            existingOrganizer.lastname = requestDto.lastname;
            existingOrganizer.OrganizationName = requestDto.OrganizationName;
            existingOrganizer.PhoneNumber = requestDto.PhoneNumber;
            existingOrganizer.profileImage = requestDto.profileImage;
            existingOrganizer.currency = requestDto.currency;
            existingOrganizer.ville = requestDto.ville;

            try
            {
                // Update user in the database
                var result = await _userManager.UpdateAsync(existingOrganizer);
                if (result.Succeeded)
                {
                    return "success";
                }
                else
                {
                    return result.Errors.FirstOrDefault()?.Description ?? "Failed to update organizer.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> AcceptOrganizer(string id)
        {
            var organizer = await GetOrganizerById2(id);
            if (organizer == null)
            {
                return false;
            }
            organizer.isActive = true;
            var result = await _userManager.UpdateAsync(organizer);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrganizer(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                // Failed to delete user
                return false;
            }

            return true; // User deleted successfully

        }

        public async Task<bool> IsOrganizerExist(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.FirstOrDefault() == "CLIENT" || roles.FirstOrDefault() == "ADMIN")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> IsOrganizerAccepted(string id)
        {
            if( await IsOrganizerExist(id))
            {
                var organizer = await _userManager.FindByIdAsync(id);
                if (organizer.isActive)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
