using EventManagementApi.Models;
using EventManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventManagementApi.Controllers
{
    [Authorize]// only authorize pesson can access it
    [ApiController] //
    [Route("api/[controller]")] //Defines the URL route for the controller
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService; 

        public EventsController(IEventService eventService)
        {
            _eventService = eventService; //_eventService is the service that handles database logic.


        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return NotFound(new { Message = "Event not found" });
            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _eventService.CreateAsync(ev);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event ev)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _eventService.UpdateAsync(id, ev);
            if (!updated) return NotFound(new { Message = "Event not found" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventService.DeleteAsync(id);
            if (!deleted) return NotFound(new { Message = "Event not found" });

            return NoContent();
        }
    }
}
