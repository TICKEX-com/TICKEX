using AutoMapper;
using event_service.DTOs;
using event_service.Entities;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace event_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                // var events = _eventService.GetEvents();
                _responseDto.Result = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEvents());
                _responseDto.Message = "Events returned";

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            try
            {
                _responseDto.Result = _mapper.Map<EventByIdDto>(await _eventService.GetEvent(id));
                _responseDto.Message = "Event returned";

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return Ok(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventReqDto ev)
        {
            try
            {
                if (await _eventService.CreateEvent(ev)) {
                    _responseDto.Result = _mapper.Map<EventReqDto>(ev);
                    _responseDto.Message = "Event saved";   
                } 
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return Ok(_responseDto);
        }
    }
}
