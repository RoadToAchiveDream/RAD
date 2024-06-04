using Microsoft.AspNetCore.Mvc;
using RAD.DTOs.Habits;
using RAD.Services.Services.Habits;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HabitsController(IHabitService habitService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] HabitCreateModel habit)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.CreateAsync(habit)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] HabitUpdateModel habit)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.UpdateAsync(id, habit)
        });
    }
}


