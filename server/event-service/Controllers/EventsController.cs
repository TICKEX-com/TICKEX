using AutoMapper;
using event_service.DTOs;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEvents());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return NotFound();
            }
        }


        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            try
            {
                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEvent(id));                    

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return NotFound();
            }
        }


        [HttpPost("Event")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev)
        {
            try
            {
                if (await _eventService.CreateEvent(ev))
                {
                    return Ok(ev);
                }
                else
                {
                    return BadRequest("Failed to create event.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Organizer/{id}/Events")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> GetEventsByOrganizer(string id)
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByOrganizer(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return NotFound();
            }
        }

    }
}
