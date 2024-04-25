using event_service.Data;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace event_service.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserService _userService;



        public EventService(DataContext context, IHttpClientFactory httpClientFactory, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Event> GetEventById(int id)
        {
            var _event = await _context.Events.Include(img => img.Images).Include(ct => ct.Category).Include(org => org.Organizer).FirstAsync(ev => ev.Id == id);
            
            if (_event.Organizer.OrganizationName.IsNullOrEmpty())
            {
                _event.Organizer.OrganizationName = _event.Organizer.firstname + " " + _event.Organizer.lastname;
            }
            return _event;
            
        }

        public async Task<ICollection<Event>> GetEvents()
        {
            return await _context.Events.Include(ct => ct.Category).OrderBy(ev => ev.Id).ToListAsync();
        }

        public async Task<bool> IsEventExist(int id)
        {
            return await _context.Events.AnyAsync(ev => ev.Id == id);
        }

        public async Task<bool> IsCategoryExist(int id)
        {
            return await _context.Categories.AnyAsync(ct => ct.Id == id);
        }

        public async Task<bool> OrganizerHasEvents(string id)
        {
            return await _context.Events.AnyAsync(ev => ev.OrganizerId == id);
        }

        public async Task<ICollection<Event>> GetEventsByOrganizer(string id)
        {
            return await _context.Events
                                 .Where(ev => ev.OrganizerId == id)
                                 .Include(ct => ct.Category)
                                 .OrderBy(ev => ev.Id)
                                 .ToListAsync();
        }

        public async Task<Event> GetEventById(string OrganizerId, int id)
        {
            return await _context.Events.Where(ev => ev.OrganizerId == OrganizerId).Include(img => img.Images).Include(ct => ct.Category).FirstAsync(ev => ev.Id == id);
        }

        public async Task<ICollection<Event>> GetEventsByCategory(int id)
        {
            return await _context.Events.Where(ct => ct.CategoryId == id).Include(ct => ct.Category).OrderBy(ev => ev.Id).ToListAsync();
        }

        public async Task<bool> CreateEvent(EventReqDto Event, string OrganizerId)
        {  
            Event ev = new()
            {
                Title = Event.Title,
                Description = Event.Description,
                Date = Event.Date,
                Location = Event.Location,
                MinPrize = Event.MinPrize,
                CategoryId = Event.CategoryId,
                OrganizerId = OrganizerId
            };


            if (!string.IsNullOrEmpty(Event.Poster))
            {
                ev.Poster = Event.Poster;
            }

            _context.Events.Add(ev);

            // Handle images
            if (Event.Images != null && Event.Images.Any())
            {
                foreach (var image in Event.Images)
                {
                    // Add image
                    var img = new Image { url = image.url, Event = ev };
                    _context.Images.Add(img);
                }
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEvent(string OrganizerId, int id)
        {
            var _event = await GetEventById(OrganizerId, id);

            if (_event == null)
                return false; // Event not found

            if (!_event.On_sell && !_event.Is_finished)
            {
                _context.Events.Remove(_event);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            else if (_event.Is_finished)
            {
                _context.Events.Remove(_event);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
  
        }

        public async Task<bool> UpdateEvent(EventReqDto ev, string OrganizerId, int EventId)
        {
            var existingEvent = await GetEventById(OrganizerId, EventId);

            if (existingEvent == null)
                return false; // Event not found

            // Update event properties
            existingEvent.Title = ev.Title;
            existingEvent.Description = ev.Description;
            existingEvent.Date = ev.Date;
            existingEvent.Location = ev.Location;
            existingEvent.MinPrize = ev.MinPrize;
            existingEvent.CategoryId = ev.CategoryId;

            // Update poster if provided
            if (!string.IsNullOrEmpty(ev.Poster))
            {
                existingEvent.Poster = ev.Poster;
            }

            // Update more images
            if (ev.Images != null && ev.Images.Any())
            {
                // Add new images
                foreach (var image in ev.Images)
                {
                    _context.Images.Add(new Image { url = image.url, Event = existingEvent });
                }
            }

            var result = await _context.SaveChangesAsync();
            return result > 0;

        }

        public async Task<ICollection<Event>> GetEventsByDate(string Date)
        {
            // Split the Date string into year, month, and day components
            string[] dateComponents = Date.Split('-');

            if (dateComponents.Length != 3)
            {
                // Handle invalid date format
                throw new ArgumentException("Invalid date format. Expected yyyy-mm-dd.");
            }

            // Parse the components to integers
            int y = int.Parse(dateComponents[0]);
            int m = int.Parse(dateComponents[1]);
            int d = int.Parse(dateComponents[2]);

            return await _context.Events
                .Where(ev => ev.Date.Year >= y && ev.Date.Month >= m && ev.Date.Day >= d)
                .Include(ct => ct.Category)
                .OrderBy(ev => ev.Id)
                .ToListAsync();
        }

        public async Task<ICollection<Event>> GetEventsByTitle(string title)
        {
            return await _context.Events
                .Where(ev => ev.Title.Contains(title))
                .Include(ct => ct.Category)
                .OrderBy(ev => ev.Id)
                .ToListAsync();
        }
    }
}
