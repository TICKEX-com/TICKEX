<<<<<<< HEAD
﻿using AutoMapper;
using event_service.DTOs;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace event_service.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        // private readonly IGreeterService _greeterService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
            // _greeterService = greeterService;
        }
        // [HttpGet("grpc")]
        // public async Task<IActionResult> TestGrpc()
        // {
        //     try
        //     {
        //         var test = await _greeterService.Hello();
        //         Console.WriteLine(test);
        //         return Ok(test);
        //     } catch (Exception ex)
        //     {
        //         return StatusCode(500, $"{ex.Message}");

        //     }
        // }

        [HttpGet("Events")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEvents());

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }


        [HttpGet("Events/id/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                if (! await _eventService.IsEventExist(id))
                    return NotFound();
                
                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(id));                    

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Type/{name}")]
        public async Task<IActionResult> GetEventsByType(string name)
        {
            try
            {
                if (!await _eventService.IsTypeExist(name))
                    return NotFound();

                var _events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByType(name));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Date/{Date}")]
        public async Task<IActionResult> GetEventsByDate(string Date)
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByDate(Date));

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Title/{title}")]
        public async Task<IActionResult> GetEventsByTitle(string title)
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByTitle(title));

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/{id}/Categories")]
        public async Task<IActionResult> GetCategoriesByEventId(int id)
        {
            try
            {
                if (!await _eventService.IsEventExist(id))
                    return NotFound();
                var cats = _mapper.Map<ICollection<CategoryDto>>(await _eventService.GetCategoriesByEventId(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(cats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
=======
﻿using AutoMapper;
using event_service.DTOs;
using event_service.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace event_service.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet("Events")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEvents());

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }


        [HttpGet("Events/id/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                if (! await _eventService.IsEventExist(id))
                    return NotFound();
                
                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(id));                    

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Category/{id}")]
        public async Task<IActionResult> GetEventsByCategory(int id)
        {
            try
            {
                if (!await _eventService.IsCategoryExist(id))
                    return NotFound();

                var _event = _mapper.Map<EventByIdDto>(await _eventService.GetEventById(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(_event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Date/{Date}")]
        public async Task<IActionResult> GetEventsByDate(string Date)
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByDate(Date));

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpGet("Events/Title/{title}")]
        public async Task<IActionResult> GetEventsByTitle(string title)
        {
            try
            {
                var events = _mapper.Map<ICollection<EventsDto>>(await _eventService.GetEventsByTitle(title));

                if (events.IsNullOrEmpty())
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}
>>>>>>> authentication
