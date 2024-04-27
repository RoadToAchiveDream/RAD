using Microsoft.AspNetCore.Mvc;
using RAD_BackEnd.DTOs.Events;
using RAD_BackEnd.Services.Services.Events;
using RAD_BackEnd.WebApi.Models;

namespace RAD_BackEnd.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await eventService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await eventService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] EventCreateModel @event)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await eventService.CreateAsync(@event)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await eventService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] EventUpdateModel @event)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await eventService.UpdateAsync(id, @event)
        });
    }
}
