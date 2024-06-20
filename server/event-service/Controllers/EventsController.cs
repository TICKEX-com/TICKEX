using AutoMapper;
using event_service.DTOs;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace event_service.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;

        }

        [HttpGet("Events")]
        public async Task<IActionResult> GetEvents(int pageNumber = 1)
        {
            try
            {
                var events = await _eventService.GetEvents(pageNumber);

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }


        [HttpGet("Events/id/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                if (! await _eventService.IsEventExist(id))
                    return NotFound();
                
                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(id));                    

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        /*[HttpGet("Events/Type/{type}")]
        public async Task<IActionResult> GetEventsByType(string type)
        {
            try
            {

                var _events = await _eventService.GetEventsByType(type);

                if (_events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }*/

        /*[HttpGet("Events/Date/{Date}")]
        public async Task<IActionResult> GetEventsByDate(string Date)
        {
            try
            {
                var events = await _eventService.GetEventsByDate(Date);

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }*/

        [HttpGet("Events/Title/{title}")]
        public async Task<IActionResult> GetEventsByTitle(string title)
        {
            try
            {
                var events = await _eventService.GetEventsByTitle(title);

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Filter")]
        public async Task<IActionResult> FilterEvents(string Date = null, string City = null, string EventType = null, float MinPrice = 0, float MaxPrice = 0, int pageNumber = 1)
        {
            try
            {
                var filteredEvents = await _eventService.FilterEvents(Date, City, EventType, MinPrice, MaxPrice, pageNumber);

                if (filteredEvents.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(filteredEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }


        [HttpGet("Events/{id}/Categories")]
        public async Task<IActionResult> GetCategoriesByEventId(int id)
        {
            try
            {
                if (!await _eventService.IsEventExist(id))
                    return NotFound();
                var cats = _mapper.Map<ICollection<CategoryDto>>(await _eventService.GetCategoriesByEventId(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(cats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
