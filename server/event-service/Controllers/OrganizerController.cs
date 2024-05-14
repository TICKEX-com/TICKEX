using AutoMapper;
using event_service.DTOs;
using event_service.Entities;
using event_service.Extensions;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Management;

namespace event_service.Controllers
{
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public OrganizerController(IEventService eventService, IMapper mapper, IUserService userService, IConfiguration configuration)
        {
            _eventService = eventService;
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
        }

        private bool AuthorizeRole(string role)
        {
            // Retrieve JWT token from the request headers
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(jwtToken))
            {
                return false;
            }

            // Initialize JwtTokenValidator with the issuer, audience, and secret key
            /*var Secret = Environment.GetEnvironmentVariable("SECRET");
            var Issuer = Environment.GetEnvironmentVariable("ISSUER");
            var Audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var tokenValidator = new JwtTokenValidator(Issuer, Audience, Secret);*/


            var jwtOptions = _configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();
            var tokenValidator = new JwtTokenValidator(jwtOptions.Issuer, jwtOptions.Audience, jwtOptions.Secret);


            // Validate JWT token and extract user roles
            var roles = tokenValidator.ValidateTokenRole(jwtToken);

            if (roles.Contains(role))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        private bool AuthorizeID(string ID)
        {
            // Retrieve JWT token from the request headers
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(jwtToken))
            {
                return false;
            }

            // Initialize JwtTokenValidator with the issuer, audience, and secret key
            var Secret = Environment.GetEnvironmentVariable("SECRET");
            var Issuer = Environment.GetEnvironmentVariable("ISSUER");
            var Audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var tokenValidator = new JwtTokenValidator(Issuer, Audience, Secret);


            /*var jwtOptions = _configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();
            var tokenValidator = new JwtTokenValidator(jwtOptions.Issuer, jwtOptions.Audience, jwtOptions.Secret);*/

            // Validate JWT token and extract user roles
            var IDs = tokenValidator.ValidateTokenID(jwtToken);

            if (IDs.Contains(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev, string OrganizerId)
        {
            try
            {
                if (AuthorizeID(OrganizerId) && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(OrganizerId))
                        return NotFound("Organizer not found");

                    if (await _eventService.CreateEvent(ev, OrganizerId))
                    {
                        return Ok(ev);
                    }
                    else
                    {
                        return BadRequest("Failed to create event.");
                    }
                }
                else
                {
                    // User does not have the required role
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Organizer/{OrganizerId}/Events")]
        public async Task<IActionResult> GetAllEvents(string OrganizerId, int pageNumber = 1)
        {
            try
            {
                if (AuthorizeID(OrganizerId) && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(OrganizerId))
                        return NotFound("Organizer not found");

                    if (!await _eventService.OrganizerHasEvents(OrganizerId))
                        return NotFound();

                    var events = await _eventService.GetEventsByOrganizer(OrganizerId, pageNumber);

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    return Ok(events);
                }
                else
                {
                    // User does not have the required role
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Organizer/{OrganizerId}/Events/{EventId}")]
        public async Task<IActionResult> GetEventById(string OrganizerId, int EventId)
        {
            try
            {
                if (AuthorizeID(OrganizerId) && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(OrganizerId))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");

                    var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(OrganizerId, EventId));

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    return Ok(_event);
                }
                else
                {
                    // User does not have the required role
                    return Unauthorized();
                } 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpDelete("Organizer/{OrganizerId}/Events/{EventId}")]
        public async Task<IActionResult> DeleteEvent(string OrganizerId, int EventId)
        {
            try
            {
                if (AuthorizeID(OrganizerId) && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(OrganizerId))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");

                    var temp = await _eventService.DeleteEvent(OrganizerId, EventId);

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (temp)
                        return Ok(temp);
                    else
                        return BadRequest("Failed to delete event.");
                }
                else
                {
                    // User does not have the required role
                    return Unauthorized();
                }            
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
        
        [HttpPut("Organizer/{OrganizerId}/Events/{EventId}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventReqDto ev, string OrganizerId, int EventId)
        {
            try
            {
                if (AuthorizeID(OrganizerId) && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(OrganizerId))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");

                    var temp = await _eventService.UpdateEvent(ev, OrganizerId, EventId);

                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (temp)
                        return Ok(temp);
                    else
                        return BadRequest("Failed to update event.");
                }
                else
                {
                    // User does not have the required role
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
