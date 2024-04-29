<<<<<<< HEAD
﻿using AutoMapper;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace event_service.Controllers
{
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrganizerController(IEventService eventService, IMapper mapper, IUserService userService)
        {
            _eventService = eventService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("Organizers")]
        public async Task<IActionResult> GetOrganizers()
        {
            try
            {
                var organizers = await _userService.GetOrganizers();

                if (organizers.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(organizers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }


        [HttpPost("Organizer/{OrganizerId}/Events")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev, string OrganizerId)
        {
            try
            {
                if (await _eventService.CreateEvent(ev, OrganizerId))
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

        [HttpGet("Organizer/{OrganizerId}/Events")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> GetAllEvents(string OrganizerId)
        {
            try
            {
                if (!await _eventService.OrganizerHasEvents(OrganizerId))
                    return NotFound();

                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByOrganizer(OrganizerId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> GetEventById(string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(OrganizerId, EventId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> DeleteEvent(string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var temp = await _eventService.DeleteEvent(OrganizerId, EventId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (temp)
                    return Ok(temp);
                else 
                    return BadRequest("Failed to delete event.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
        
        [HttpPut("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventReqDto ev, string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var temp = await _eventService.UpdateEvent(ev, OrganizerId, EventId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (temp)
                    return Ok(temp);
                else
                    return BadRequest("Failed to update event.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
=======
﻿using AutoMapper;
using event_service.DTOs;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace event_service.Controllers
{
    [ApiController]
    [Authorize(Roles = "ORGANIZER")]
    public class OrganizerController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public OrganizerController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }


        [HttpPost("Organizer/{OrganizerId}/Events")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev, string OrganizerId)
        {
            try
            {
                if (await _eventService.CreateEvent(ev, OrganizerId))
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

        [HttpGet("Organizer/{OrganizerId}/Events")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> GetAllEvents(string OrganizerId)
        {
            try
            {
                if (!await _eventService.OrganizerHasEvents(OrganizerId))
                    return NotFound();

                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByOrganizer(OrganizerId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> GetEventById(string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(OrganizerId, EventId));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> DeleteEvent(string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var temp = await _eventService.DeleteEvent(OrganizerId, EventId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (temp)
                    return Ok(temp);
                else 
                    return BadRequest("Failed to delete event.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
        
        [HttpPut("Organizer/{OrganizerId}/Events/{EventId}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventReqDto ev, string OrganizerId, int EventId)
        {
            try
            {
                if (!await _eventService.IsEventExist(EventId))
                    return NotFound();

                var temp = await _eventService.UpdateEvent(ev, OrganizerId, EventId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (temp)
                    return Ok(temp);
                else
                    return BadRequest("Failed to update event.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
>>>>>>> authentication
