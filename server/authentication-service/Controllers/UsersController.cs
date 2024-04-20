using authentication_service.DTOs;
using authentication_service.Services;
using authentication_service.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IProducerService producerService, IMapper mapper)
        {
            _userService = userService;
            _producerService = producerService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }


        /*[HttpGet("organizer/{topic}/{username}")]
        public async Task<IActionResult> GetOrganizerByUsername(string topic, string username)
        {
            try 
            {
                var organizer = await _userService.GetOrganizerByUsername(username);
                if (organizer != null)
                {
                    await _producerService.publish(topic, organizer);
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
                var organizers = await _userService.GetOrganizers();
                if (!organizers.IsNullOrEmpty())
                {
                    // await _producerService.publish("Tickex", organizer);
                    _responseDto.Result = organizers;
                    _responseDto.Message = "Success";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.Message = "Organizers table is empty";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("organizer/{id}")]
        public async Task<IActionResult> GetOrganizerById(string id)
        {
            try
            {
                var organizer = await _userService.GetOrganizerById(id);
                if (organizer != null)
                {
                    // await _producerService.publish("Tickex", organizer);
                    _responseDto.Result = organizer;
                    _responseDto.Message = "Message send successfuly";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.Message = "Organizer doesn't exist";
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPost("Update/Organizer/{id}")]
        [Authorize(Roles = "ORGANIZER")]
        public async Task<IActionResult> UpdateOrganizer([FromBody] UpdateReqOrganizerDto requestDto, string id)
        {
            try
            {
                var errorMessage = await _userService.UpdateOrganizer(requestDto, id);
                if (errorMessage != "success")
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = errorMessage;
                    return BadRequest(_responseDto);
                }
                var organizer = _mapper.Map<OrganizerDto>(requestDto);
                organizer.Id = id;
                await _producerService.publish("Tickex", organizer);
                _responseDto.Result = organizer;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }
    }
}
