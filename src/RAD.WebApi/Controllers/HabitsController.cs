using Microsoft.AspNetCore.Mvc;
using RAD.DTOs.Habits;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.Habits;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HabitsController(IHabitApiService habitService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] HabitCreateModel habit)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await habitService.PostAsync(habit)
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
            Data = await habitService.PutAsync(id, habit)
        });
    }
}


