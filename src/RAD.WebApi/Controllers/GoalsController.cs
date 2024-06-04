using Microsoft.AspNetCore.Mvc;
using RAD.DTOs.Goals;
using RAD.Services.Services.Goals;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalsController(IGoalService goalService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] GoalCreateModel goal)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await goalService.CreateAsync(goal)
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
            Data = await goalService.UpdateAsync(id, goal)
        });
    }
}
