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

        [HttpPost]
        public IActionResult GetOrganizerByUsername()
        {
            return Ok();
        }
    }
}
