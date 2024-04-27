using Microsoft.AspNetCore.Mvc;
using RAD_BackEnd.DTOs.Tasks;
using RAD_BackEnd.Services.Services.Tasks;
using RAD_BackEnd.WebApi.Models;

namespace RAD_BackEnd.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] TaskCreateModel task)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.CreateAsync(task)
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
            Data = await taskService.UpdateAsync(id, task)
        });
    }
}

