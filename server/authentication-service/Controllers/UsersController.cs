using authentication_service.DTOs;
using authentication_service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace authentication_service.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private readonly IUserService _userService;
        private readonly IProducerService _producerService;

        public UsersController(IUserService userService, IProducerService producerService)
        {
            _userService = userService;
            _producerService = producerService;
            _responseDto = new ResponseDto();
        }


        [HttpPost("organizer/{topic}/{username}")]
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
        }
    }
}
