using event_service.Data;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;

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
            var _event = await _context.Events.Include(ev => ev.Images).Include(ev => ev.Organizer).Include(ev => ev.Categories).FirstAsync(ev => ev.Id == id);
            
            if (_event.Organizer.OrganizationName.IsNullOrEmpty())
            {
                _event.Organizer.OrganizationName = _event.Organizer.firstname + " " + _event.Organizer.lastname;
            }
            return _event;
            
        }

        private async Task<ICollection<EventsDto>> AddMinPrice(List<Event> events)
        {
            ICollection<EventsDto> _events = new List<EventsDto>();

            foreach (var ev in events)
            {
                var category = ev.Categories.FirstOrDefault();
                EventsDto eve = new EventsDto
                {
                    Id = ev.Id,
                    Title = ev.Title,
                    EventType = ev.EventType,
                    EventDate = ev.EventDate,
                    City = ev.City,
                    Poster = ev.Poster,
                    MinPrice = category != null ? category.Price : 0,
                };

                _events.Add(eve);
            }

            return _events;
        }

        public async Task<ICollection<EventsDto>> GetEvents(int pageNumber)
        {
            var events = await _context.Events
                .Include(ev => ev.Categories.OrderBy(cat => cat.Price))
                .OrderBy(ev => ev.EventDate)
                .Skip((pageNumber - 1) * 6)
                .Take(6)
                .ToListAsync();

            var _events = await AddMinPrice(events);

            return _events;
        }



        public async Task<bool> IsEventExist(int id)
        {
            return await _context.Events.AnyAsync(ev => ev.Id == id);
        }

        public async Task<bool> IsTypeExist(string type)
        {
            return await _context.Events.AnyAsync(ev => ev.EventType == type);
        }

        public async Task<bool> OrganizerHasEvents(string id)
        {
            return await _context.Events.AnyAsync(ev => ev.OrganizerId == id);
        }

        public async Task<ICollection<EventsDto>> GetEventsByOrganizer(string id, int pageNumber)
        {
            var events = await _context.Events
                                 .Where(ev => ev.OrganizerId == id)
                                 .Include(ev => ev.Categories.OrderBy(cat => cat.Price))
                                 .OrderBy(ev => ev.EventDate)
                                 .Skip((pageNumber - 1) * 6)
                                 .Take(6)
                                 .ToListAsync();

            var _events = await AddMinPrice(events);

            return _events;
        }

        public async Task<Event> GetEventById(string OrganizerId, int id)
        {
            return await _context.Events.Where(ev => ev.OrganizerId == OrganizerId).Include(img => img.Images).FirstAsync(ev => ev.Id == id);
        }

        public async Task<bool> CreateEvent(EventReqDto Event, string OrganizerId)
        {
            var parsedDate = DateTime.ParseExact(Event.EventDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);


            Event ev = new()
            {
                Title = Event.Title,
                Description = Event.Description,
                Duration = Event.Duration,
                Time = Event.Time,
                CreationDate = DateTime.Now,
                EventDate = parsedDate,
                City = Event.City,
                Address = Event.Address,
                EventType = Event.EventType,
                Poster = Event.Poster,
                OrganizerId = OrganizerId,
                DesignId = Event.DesignId
            };


            
            

            _context.Events.Add(ev);

            // Handle images
            /*if (Event.Images != null && Event.Images.Any())
            {
                foreach (var image in Event.Images)
                {
                    // Add image
                    var img = new Image { url = image.url, Event = ev };
                    _context.Images.Add(img);
                }
            }*/

            if (Event.Categories != null && Event.Categories.Any())
            {
                foreach (var category in Event.Categories)
                {
                    var cat = new Category {
                        Name = category.Name, 
                        Seats = category.Seats,
                        Price = category.Price,
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
            var parsedDate = DateTime.ParseExact(ev.EventDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);

            if (existingEvent == null)
                return false; // Event not found

            // Update event properties
            existingEvent.Title = ev.Title;
            existingEvent.Description = ev.Description;
            existingEvent.Duration = ev.Duration;
            existingEvent.Time = ev.Time;
            existingEvent.EventDate = parsedDate;
            existingEvent.City = ev.City;
            existingEvent.Address = ev.Address;
            existingEvent.EventType = ev.EventType;
            existingEvent.DesignId = ev.DesignId;
            existingEvent.Poster = ev.Poster;
            

            // Update more images
            /*if (ev.Images != null && ev.Images.Any())
            {
                // Add new images
                foreach (var image in ev.Images)
                {
                    _context.Images.Add(new Image { url = image.url, Event = existingEvent });
                }
            }*/

            var result = await _context.SaveChangesAsync();
            return result > 0;

        }

        public async Task<ICollection<EventsDto>> GetEventsByDate(string Date)
        {
            var parsedDate = DateTime.ParseExact(Date, "MM-dd-yyyy", CultureInfo.InvariantCulture);

            var events = await _context.Events
                 .Where(ev => ev.EventDate >= parsedDate)
                .Include(ev => ev.Categories)
                .OrderBy(ev => ev.Id)
                .ToListAsync();

            var _events = await AddMinPrice(events);

            return _events;
        }

        public async Task<ICollection<EventsDto>> GetEventsByTitle(string title)
        {
            var events = await _context.Events
                .Where(ev => ev.Title.Contains(title))
                .Include(ev => ev.Categories)
                .OrderBy(ev => ev.Id)
                .ToListAsync();

            var _events = await AddMinPrice(events);

            return _events;
        }
        public async Task<ICollection<EventsDto>> GetEventsByType(string type)
        {
            var events = await _context.Events
                                .Where(ev => ev.EventType.Contains(type))
                                .Include(ev => ev.Categories)
                                .OrderBy(ev => ev.Id)
                                .ToListAsync();

            var _events = await AddMinPrice(events);

            return _events;
        }

        public async Task<ICollection<Category>> GetCategoriesByEventId(int id)
        {
            return await _context.Categories.Where(ev => ev.EventId == id).OrderBy(ev => ev.Id).ToListAsync();
        }



        public async Task<ICollection<EventsDto>> FilterEvents(string Date, string City, string EventType, float MinPrice, float MaxPrice, int pageNumber)
        {

            var query = _context.Events.Include(ev => ev.Categories).AsQueryable();

            // Apply filters based on provided parameters
            if (!string.IsNullOrEmpty(Date))
            {
                // Assuming Date is a string in a specific format, you need to parse it to DateTime
                var parsedDate = DateTime.ParseExact(Date, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                query = query.Where(ev => ev.EventDate >= parsedDate);
            }

            if (!string.IsNullOrEmpty(City))
            {
                query = query.Where(ev => ev.City.Contains(City));
            }

            if (!string.IsNullOrEmpty(EventType))
            {
                query = query.Where(ev => ev.EventType.Contains(EventType));
            }
            if (MinPrice < MaxPrice)
            {
                if (MinPrice > 0)
                {
                    // Filter events based on minimum price of categories
                    query = query.Where(ev => ev.Categories.Any(cat => cat.Price >= MinPrice));
                }

                if (MaxPrice > 0)
                {
                    // Filter events based on maximum price of categories
                    query = query.Where(ev => ev.Categories.Any(cat => cat.Price <= MaxPrice));
                }
            }

            if (string.IsNullOrEmpty(Date) && string.IsNullOrEmpty(City) && string.IsNullOrEmpty(EventType) && MinPrice == 0 && MaxPrice == 0)
            {
                return null;
            }

            // Execute the query and retrieve the filtered events
            var filteredEvents = await query.OrderBy(ev => ev.EventDate).Skip((pageNumber - 1) * 6).Take(6).ToListAsync();

            // Convert the filtered events to DTOs
            var eventsDto = await AddMinPrice(filteredEvents);

            return eventsDto;
        }


    }
}
