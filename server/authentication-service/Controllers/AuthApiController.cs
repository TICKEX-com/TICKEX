﻿using authentication_service.DTOs;
using authentication_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace authentication_service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ResponseDto _response;

        public AuthApiController(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _response = new();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Register/Client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterReqClientDto requestDto)
        {
            var errorMessage = await _authService.RegisterClient(requestDto);
            if (errorMessage != "success")
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("Register/Organizer")]
        public async Task<IActionResult> RegisterOrganizer([FromBody] RegisterReqOrganizerDto requestDto)
        {
            var errorMessage = await _authService.RegisterOrganizer(requestDto);
            if (errorMessage != "success")
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var loginResponse = await _authService.Login(_httpContextAccessor.HttpContext, loginRequest);
                if (loginResponse.User == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Username or password is incorrect";
                    return BadRequest(_response);
                }
                _response.Result = loginResponse;
                return Ok(_response);
            } catch(Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
            
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (await _authService.Logout(_httpContextAccessor.HttpContext))
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleReqDto requestDto)
        {
            var assignRoleSuccessful = await _authService.AssignRole(requestDto.Email, requestDto.roleName.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error Encoutered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}

