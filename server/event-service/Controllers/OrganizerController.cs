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
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public OrganizerController(IEventService eventService, IMapper mapper, IUserService userService, IConfiguration configuration, IProducerService producerService)
        {
            _eventService = eventService;
            _mapper = mapper;
            _userService = userService;
            _configuration = configuration;
            _producerService = producerService;
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

        private string? AuthorizeID()
        {
            // Retrieve JWT token from the request headers
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];
            if (string.IsNullOrEmpty(jwtToken))
            {
                return null;
            }

            // Initialize JwtTokenValidator with the issuer, audience, and secret key
            /*var Secret = Environment.GetEnvironmentVariable("SECRET");
            var Issuer = Environment.GetEnvironmentVariable("ISSUER");
            var Audience = Environment.GetEnvironmentVariable("AUDIENCE");
            var tokenValidator = new JwtTokenValidator(Issuer, Audience, Secret);*/


            var jwtOptions = _configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();
            var tokenValidator = new JwtTokenValidator(jwtOptions.Issuer, jwtOptions.Audience, jwtOptions.Secret);

            // Validate JWT token and extract user roles
            var ID = tokenValidator.ValidateTokenID(jwtToken);

            return ID[0];
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


        [HttpPost("Organizer/CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && AuthorizeRole("ORGANIZER"))
                {
                    if (!await _userService.IsOrganizerExists(id))
                        return NotFound("Organizer not found");

                    if (await _producerService.TestKafkaConnectionAsync())
                    {
                        var eventid = await _eventService.CreateEvent(ev, id);
                        if (eventid!=null)
                        {
                            var _event = _mapper.Map<PublishDto>(ev);
                            _event.Id = (int)eventid;
                            _event.OrganizerId = id;
                            await _producerService.publish("events", _event);
                            return Ok(_event);   
                        }
                        else
                        {
                            return BadRequest("Failed to create event.");
                        }
                    } else
                    {   
                        return BadRequest("Connection failure to Kafka");
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

        [HttpGet("Organizer/Events")]
        public async Task<IActionResult> GetAllEvents(int pageNumber = 1)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(id))
                        return NotFound("Organizer not found");

                    if (!await _eventService.OrganizerHasEvents(id))
                        return NotFound();

                    var events = await _eventService.GetEventsByOrganizer(id, pageNumber);

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

        [HttpGet("Organizer/Events/{EventId}")]
        public async Task<IActionResult> GetEventById(int EventId)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(id))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");

                    var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(id, EventId));

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

        [HttpDelete("Organizer/DeleteEvent/{EventId}")]
        public async Task<IActionResult> DeleteEvent(int EventId)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(id))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");

                    var temp = await _eventService.DeleteEvent(id, EventId);

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
        
        [HttpPut("Organizer/UpdateEvent/{EventId}")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventReqDto ev, int EventId)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && (AuthorizeRole("ORGANIZER") || AuthorizeRole("ADMIN")))
                {
                    if (!await _userService.IsOrganizerExists(id))
                        return NotFound("Organizer not found");

                    if (!await _eventService.IsEventExist(EventId))
                        return NotFound("Event not found");


                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    if (await _producerService.TestKafkaConnectionAsync())
                    {
                        var eventid = await _eventService.UpdateEvent(ev, id, EventId);
                        if (eventid!=null)
                        {
                            var _event = _mapper.Map<PublishDto>(ev);
                            _event.Id = (int)eventid;
                            _event.OrganizerId = id;
                            await _producerService.publish("events", _event);
                            return Ok(_event);   
                        }
                        else
                        {
                            return BadRequest("Failed to update event.");
                        }
                    } else
                    {   
                        return BadRequest("Connection failure to Kafka");
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
    }
}
