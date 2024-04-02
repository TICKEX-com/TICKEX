using AutoMapper;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace event_service.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
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


        [HttpGet("{id}")]
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


        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev)
        {
            try
            {
                if (await _eventService.CreateEvent(ev))
                {
                    var createdEvent = _mapper.Map<EventReqDto>(ev);
                    return Ok(createdEvent);
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

    }
}
