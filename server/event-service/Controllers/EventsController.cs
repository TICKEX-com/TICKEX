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
        public IActionResult GetEvents()
        {
            try
            {
                // var events = _eventService.GetEvents();
                _responseDto.Result = _mapper.Map<ICollection<EventDTO>>(_eventService.GetEvents());
            } catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            
            return Ok(_responseDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            try
            {
                _responseDto.Result = _mapper.Map<EventDTO>(_eventService.GetEvent(id));
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
