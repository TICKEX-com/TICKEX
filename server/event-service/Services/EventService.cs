using event_service.Data;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            var _event = await _context.Events.Include(img => img.Images).Include(org => org.Organizer).FirstAsync(ev => ev.Id == id);
            
            if (_event.Organizer.OrganizationName.IsNullOrEmpty())
            {
                _event.Organizer.OrganizationName = _event.Organizer.firstname + " " + _event.Organizer.lastname;
            }
            return _event;
            
        }

        public async Task<ICollection<Event>> GetEvents()
        {
            return await _context.Events.OrderBy(ev => ev.Id).ToListAsync();
        }

        public async Task<bool> IsEventExist(int id)
        {
            return await _context.Events.AnyAsync(ev => ev.Id == id);
        }

        public async Task<bool> IsTypeExist(string name)
        {
            return await _context.Events.AnyAsync(ev => ev.EventType == name);
        }

        public async Task<bool> OrganizerHasEvents(string id)
        {
            return await _context.Events.AnyAsync(ev => ev.OrganizerId == id);
        }

        public async Task<ICollection<Event>> GetEventsByOrganizer(string id)
        {
            return await _context.Events
                                 .Where(ev => ev.OrganizerId == id)
                                 .OrderBy(ev => ev.Id)
                                 .ToListAsync();
        }

        public async Task<Event> GetEventById(string OrganizerId, int id)
        {
            return await _context.Events.Where(ev => ev.OrganizerId == OrganizerId).Include(img => img.Images).FirstAsync(ev => ev.Id == id);
        }

        public async Task<ICollection<Event>> GetEventsByType(string name)
        {
            return await _context.Events.Where(ev => ev.EventType == name).OrderBy(ev => ev.Id).ToListAsync();
        }

        public async Task<bool> CreateEvent(EventReqDto Event, string OrganizerId)
        {  
            Event ev = new()
            {
                Title = Event.Title,
                Description = Event.Description,
                Duration = Event.Duration,
                Time = Event.Time,
                Date = DateTime.Now,
                City = Event.City,
                Address = Event.Address,
                EventType = Event.EventType,
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

            if (Event.Categories != null && Event.Categories.Any())
            {
                foreach (var category in Event.Categories)
                {
                    var cat = new Category {
                        Name = category.Name, 
                        Seats = category.Seats,
                        Prize = category.Prize,
                        Color = category.Color,
                        Event = ev};
                        _context.Categories.Add(cat);
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
            existingEvent.Duration = ev.Duration;
            existingEvent.Time = ev.Time;
            existingEvent.Date = DateTime.Now;
            existingEvent.City = ev.City;
            existingEvent.Address = ev.Address;
            existingEvent.EventType = ev.EventType;

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
            Console.WriteLine(dateComponents[0]);
            if (dateComponents.Length != 3)
            {
                // Handle invalid date format
                throw new ArgumentException("Invalid date format. Expected mm-dd-yyyy.");
            }

            // Parse the components to integers
            int m = int.Parse(dateComponents[0]);
            int d = int.Parse(dateComponents[1]);
            int y = int.Parse(dateComponents[2]);

            return await _context.Events
                .Where(ev => ev.Date.Year >= y && ev.Date.Month >= m && ev.Date.Day >= d)
                .OrderBy(ev => ev.Id)
                .ToListAsync();
        }

        public async Task<ICollection<Event>> GetEventsByTitle(string title)
        {
            return await _context.Events
                .Where(ev => ev.Title.Contains(title))
                .OrderBy(ev => ev.Id)
                .ToListAsync();
        }

        public async Task<ICollection<Category>> GetCategoriesByEventId(int id)
        {
            return await _context.Categories.Where(ev => ev.EventId == id).OrderBy(ev => ev.Id).ToListAsync();
        }
    
    }
}
