using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.Tasks;
using RAD.WebApi.DTOs.Tasks;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TasksController(ITaskApiService taskService) : ControllerBase
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
            Data = await taskService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] TaskCreateModel task)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.PostAsync(task)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] TaskUpdateModel task)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.PutAsync(id, task)
        });
    }
}

