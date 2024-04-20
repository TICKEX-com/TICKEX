using event_service.Data;
using event_service.Entities;
using event_service.Services.IServices;
using Google.Api.Gax.ResourceNames;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace event_service.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<Organizer> GetOrganizerById(string id)
        {
            return await _context.Organizers.FirstAsync(org => org.Id == id);
        }

        public async Task<bool> IsOrganizerExists(string id)
        {
            return await _context.Organizers.AnyAsync(org => org.Id == id);
        }

        public async Task<bool> CreateOrganizer(Organizer organizer)
        {
            Organizer org = new()
            {
                Id = organizer.Id,
                Email = organizer.Email,
                firstname = organizer.firstname,
                lastname = organizer.lastname,
                PhoneNumber = organizer.PhoneNumber,
                OrganizationName = organizer.OrganizationName
            };
            
            _context.Organizers.Add(org);
            
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateOrganizer(Organizer organizer)
        {
            var existingOrganizer = await GetOrganizerById(organizer.Id);

            if (existingOrganizer == null)
                return false; // Organizer not found

            // Update event properties
            existingOrganizer.OrganizationName = organizer.OrganizationName;
            existingOrganizer.PhoneNumber = organizer.PhoneNumber;
            existingOrganizer.firstname = organizer.firstname;
            existingOrganizer.lastname = organizer.lastname;
            existingOrganizer.Email = organizer.Email;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<ICollection<Organizer>> GetOrganizers()
        {
            return await _context.Organizers.ToListAsync();
        }
    }
}
