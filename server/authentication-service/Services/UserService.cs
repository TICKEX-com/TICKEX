using authentication_service.Data;
using authentication_service.DTOs;
using authentication_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace authentication_service.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly ResponseDto _responseDto;

        public UserService(DataContext dataContext) 
        {
            _dataContext = dataContext;
            _responseDto = new ResponseDto();
        }

        public async Task<bool> GetClientByUsername(string username)
        {
            var client = await _dataContext.Clients.ToListAsync();
            if(client.Any())
            {
                return true;
            }else
            {
                return false;
            }
        }

        public async Task<bool> GetOrganizerByUsername(string username)
        {
            var organizer = await _dataContext.Organizers.ToListAsync();
            if (organizer.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
