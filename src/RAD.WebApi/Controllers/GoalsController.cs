using Microsoft.AspNetCore.Mvc;
using RAD.DTOs.Goals;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.Goals;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalsController(IGoalApiService goalService) : ControllerBase
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
            Data = await goalService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] GoalCreateModel goal)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.PostAsync(goal)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] GoalUpdateModel goal)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.PutAsync(id, goal)
        });
    }
}
