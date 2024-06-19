using authentication_service.DTOs;
using authentication_service.Entities;
using authentication_service.Extensions;
using authentication_service.Services;
using authentication_service.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace authentication_service.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private readonly IUserService _userService;
        private readonly IProducerService _producerService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IProducerService producerService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _producerService = producerService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _configuration = configuration;
        }

        private bool Authorize(string role)
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
            var roles = tokenValidator.ValidateToken(jwtToken);

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


        /*[HttpGet("organizer/{topic}/{username}")]
        public async Task<IActionResult> GetOrganizerByUsername(string topic, string username)
        {
            try 
            {
                var organizer = await _userService.GetOrganizerByUsername(username);
                if (organizer != null)
                {
                    await _producerService.Publish(topic, organizer);
                    _responseDto.Result = organizer;
                    _responseDto.Message = "Message send successfuly";
                    return Ok(_responseDto);
                }else
                {
                    _responseDto.Message = "Organizer doesn't exist";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }*/

        [HttpGet("Organizers")]
        public async Task<IActionResult> GetOrganizers()
        {
            try
            {
                // Check if the user has the required role ("ADMIN")
                if (Authorize("ADMIN"))
                {
                    // User is authorized, proceed with fetching organizers
                    var organizers = await _userService.GetOrganizers();

                    if (organizers.Any())
                    {
                        // _responseDto.Result = organizers;
                        // _responseDto.Message = "Success";
                        return Ok(organizers);
                    }
                    else
                    {
                        // _responseDto.Message = "Organizers table is empty";
                        // _responseDto.IsSuccess = false;
                        return BadRequest("Organizers table is empty");
                    }
                }
                else
                {
                    // User does not have the required role
                    // _responseDto.IsSuccess = false;
                    // _responseDto.Message = "Unauthorized";
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // _responseDto.IsSuccess = false;
                // _responseDto.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Organizer/{id}")]
        public async Task<IActionResult> GetOrganizerById(string id)
        {
            try
            {
                // Check if the user has the required role ("ADMIN")
                if (Authorize("ADMIN"))
                {
                    var organizer = await _userService.GetOrganizerById(id);
                    organizer.Role = "ORGANIZER";
                    if (organizer != null)
                    {
                        // await _producerService.Publish("Tickex", organizer);
                        // _responseDto.Result = organizer;
                        // _responseDto.Message = "Organizer found successfully";
                        return Ok(organizer);
                    }
                    else
                    {
                        // _responseDto.Message = "Organizer doesn't exist";
                        // _responseDto.IsSuccess = false;
                        return BadRequest("Organizer doesn't exist");
                    }
                }
                else
                {
                    // User does not have the required role
                    // _responseDto.IsSuccess = false;
                    // _responseDto.Message = "Unauthorized";
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // _responseDto.IsSuccess = false;
                // _responseDto.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Organizer/Profil")]
        public async Task<IActionResult> GetOrganizerById()
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && Authorize("ORGANIZER"))
                {
                    var organizer = await _userService.GetOrganizerById(id);
                    organizer.Role = "ORGANIZER";
                    if (organizer != null)
                    {
                        // await _producerService.Publish("Tickex", organizer);
                        _responseDto.Result = organizer;
                        _responseDto.Message = "Organizer found successfully";
                        return Ok(organizer);
                    }
                    else
                    {
                        _responseDto.Message = "Organizer doesn't exist";
                        _responseDto.IsSuccess = false;
                        return BadRequest("Organizer doesn't exist");
                    }
                }
                else
                {
                    // User does not have the required role
                    /*_responseDto.IsSuccess = false;
                    _responseDto.Message = "Unauthorized";*/
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                /*_responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;*/
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Organizer/UpdateProfil")]
        public async Task<IActionResult> UpdateOrganizer([FromBody] UpdateReqOrganizerDto requestDto)
        {
            try
            {
                var id = AuthorizeID();
                if (id!=null && Authorize("ORGANIZER"))
                {
                    if (!await _userService.IsOrganizerExist(id))
                    {
                        /*_responseDto.IsSuccess = false;
                        _responseDto.Message = "Organizer not found";*/
                        return NotFound("Organizer not found");
                    }

                    var organizer = _mapper.Map<OrganizerDto>(requestDto);
                    organizer.Id = id;
                    organizer.isActive = await _userService.IsOrganizerAccepted(id);
                    if (organizer.isActive)
                    {
                        if (await _producerService.TestKafkaConnectionAsync())
                        {                            
                            var errorMessage = await _userService.UpdateOrganizer(requestDto, id);
                            var organizer2 = await _userService.GetOrganizerById(id);
                            if (errorMessage != "success")
                            {
                                /*_responseDto.IsSuccess = false;
                                _responseDto.Message = errorMessage;*/
                                return BadRequest(errorMessage);
                            } else {
                                await _producerService.Publish("users", organizer2);
                                /*_responseDto.Message = "the organizer updated";
                                _responseDto.Result = organizer2;*/
                                return Ok(organizer2);
                            }
                        }                        
                        else
                        {
                            // _responseDto.Message = "Connection failure to Kafka";
                            // _responseDto.IsSuccess = false;
                            return BadRequest("Connection failure to Kafka");
                        }
                    } else {
                        /*_responseDto.Message = "the organizer isn't accepted";
                        _responseDto.IsSuccess = false;*/
                        return BadRequest("the organizer isn't accepted");
                    }
                }
                else
                {
                    // User does not have the required role
                    /*_responseDto.IsSuccess = false;
                    _responseDto.Message = "Unauthorized";*/
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut("Accept/Organizer/{id}")]
        public async Task<IActionResult> AcceptOrganizer(string id)
        {
            try
            {
                // Check if the user has the required role ("ADMIN")
                if (Authorize("ADMIN"))
                {
                    if (!await _userService.IsOrganizerExist(id))
                    {
                        // _responseDto.IsSuccess = false;
                        // _responseDto.Message = "Organizer not found";
                        return NotFound("Organizer not found");
                    }

                    var organizer = await _userService.GetOrganizerById(id);

                    if (organizer.isActive)
                    {
                        // _responseDto.Message = "Organizer is already accepted";
                        // _responseDto.Result = organizer;
                        return Ok("Organizer is already accepted");
                    }

                    if (await _producerService.TestKafkaConnectionAsync())
                    {
                        var isAccepted = await _userService.AcceptOrganizer(id);
                        if (!isAccepted)
                        {
                            // _responseDto.IsSuccess = false;
                            // _responseDto.Message = "Organizer not accepted";
                            return BadRequest("Organizer not accepted");
                        }
                        else
                        {
                            organizer.isActive = true;
                            await _producerService.Publish("users", organizer);
                            // _responseDto.Message = "Organizer is accepted";
                            // _responseDto.Result = organizer;
                            return Ok("Organizer is accepted");
                        }                        
                    } else {
                        // _responseDto.Message = "Connection failure to Kafka";
                        // _responseDto.IsSuccess = false;
                        return BadRequest("Connection failure to Kafka");
                    }             
                }
                else
                {
                    // User does not have the required role
                    // _responseDto.IsSuccess = false;
                    // _responseDto.Message = "Unauthorized";
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Organizer/{id}")]
        public async Task<IActionResult> DeleteOrganizer(string id)
        {
            try
            {
                // Check if the user has the required role ("ADMIN")
                if (Authorize("ADMIN"))
                {
                    var isDeleted = await _userService.DeleteOrganizer(id);
                    if (!isDeleted)
                    {
                        // _responseDto.IsSuccess = false;
                        // _responseDto.Message = "Organizer not deleted";
                        return BadRequest("Organizer not deleted");
                    }
                    _responseDto.Message = "Organizer is deleted";
                    return Ok("Organizer is deleted");
                }
                else
                {
                    // User does not have the required role
                    // _responseDto.IsSuccess = false;
                    // _responseDto.Message = "Unauthorized";
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // _responseDto.IsSuccess = false;
                // _responseDto.Message = ex.Message;
                return BadRequest(ex.Message);
            }
        }
    }
}
