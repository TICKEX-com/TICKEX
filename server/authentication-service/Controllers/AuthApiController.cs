﻿using authentication_service.DTOs;
using authentication_service.Services;
using authentication_service.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace authentication_service.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ResponseDto _response;
        private readonly IProducerService _producerService;
        private readonly IMapper _mapper;



        public AuthApiController(IAuthService authService, IHttpContextAccessor httpContextAccessor, IProducerService producerService, IMapper mapper, IUserService userService)
        {
            _authService = authService;
            _producerService = producerService;
            _response = new();
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("Register/Client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterReqClientDto requestDto)
        {
            try
            {
                var errorMessage = await _authService.RegisterClient(requestDto);
                if (errorMessage != "success")
                {
                    _response.IsSuccess = false;
                    _response.Message = errorMessage;
                    return BadRequest(errorMessage);
                }
                var client = _mapper.Map<ClientDto>(requestDto);
                client.Id = await _userService.GetClientIdByUsername(requestDto.Username);
                client.Role = "CLIENT";
                _response.Result = client;
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost("Register/Organizer")]
        public async Task<IActionResult> RegisterOrganizer([FromBody] RegisterReqOrganizerDto requestDto)
        {
            try
            {
                var errorMessage = await _authService.RegisterOrganizer(requestDto);
                if (errorMessage != "success")
                {
                    _response.IsSuccess = false;
                    _response.Message = errorMessage;
                    return BadRequest(errorMessage);
                }
                var organizer = _mapper.Map<OrganizerDto>(requestDto);
                organizer.Id = await _userService.GetOrganizerIdByUsername(requestDto.Username);
                organizer.Role = "ORGANIZER";
                _response.Result = organizer;
                return Ok(organizer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
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
                    return BadRequest("Username or password is incorrect");
                }
                _response.Result = loginResponse;
                var user = loginResponse.User;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            if (await _authService.Logout(_httpContextAccessor.HttpContext))
            {
                return Ok("You are logged out");
            }
            return BadRequest();
        }


        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleReqDto requestDto)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");

            }

        }
    }
}

