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
        [Authorize(Roles = "ADMIN")]
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

        [HttpPut("Organizer/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateOrganizer([FromBody] UpdateReqOrganizerDto requestDto, string id)
        {
            try
            { 
                if (!await _userService.IsOrganizerExist(id))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Organizer not found";
                    return NotFound(_responseDto);
                }

                var organizer = _mapper.Map<OrganizerDto>(requestDto);
                organizer.Id = id;
                organizer.isActive = await _userService.IsOrganizerAccepted(id);
                if (organizer.isActive)
                {
                    var response = await _producerService.publish("Tickex", organizer);
                    if (response)
                    {
                        Console.WriteLine("the organizer is published");
                        var errorMessage = await _userService.UpdateOrganizer(requestDto, id);
                        if (errorMessage != "success")
                        {
                            _responseDto.IsSuccess = false;
                            _responseDto.Message = errorMessage;
                            return BadRequest(_responseDto);
                        }
                        _responseDto.Message = "the organizer is published and it is updated in DB";
                        _responseDto.Result = organizer;
                        return Ok(_responseDto);
                    }
                    else
                    {
                        _responseDto.Message = "the organizer isn't updated in DB due to connection failure to Kafka";
                        _responseDto.IsSuccess = false;
                        return BadRequest(_responseDto);
                    }
                }
                _responseDto.Message = "the organizer isn't accepted";
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpPut("Accept/Organizer/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AcceptOrganizer(string id)
        {
            try
            {
                if ( ! await _userService.IsOrganizerExist(id))
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Organizer not found";
                    return NotFound(_responseDto);
                }

                var organizer = await _userService.GetOrganizerById(id);

                if ( organizer.isActive ) {
                    _responseDto.Message = "Organizer is already accepted";
                    _responseDto.Result = organizer;
                    return Ok(_responseDto);
                }

                organizer.isActive = true;
                var response = await _producerService.publish("Tickex", organizer);
                if (response)
                {
                    var isAccepted = await _userService.AcceptOrganizer(id);
                    if (!isAccepted)
                    {
                        _responseDto.IsSuccess = false;
                        _responseDto.Message = "Organizer not accepted";
                        return BadRequest(_responseDto);
                    }
                    else
                    {
                        _responseDto.Message = "Organizer is accepted";
                        _responseDto.Result = organizer;
                        return Ok(_responseDto);
                    }
                }
                _responseDto.Message = "the organizer isn't accepted in DB due to connection failure to Kafka";
                _responseDto.Result = organizer;
                return BadRequest(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpDelete("Organizer/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteOrganizer(string id)
        {
            try
            {
                var isdeleted = await _userService.DeleteOrganizer(id);
                if (!isdeleted)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Organizer not deleted";
                    return BadRequest(_responseDto);
                }
                _responseDto.Message = "Organizer is deleted";
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
