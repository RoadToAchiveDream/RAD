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



    [HttpPatch("set-status/{id:long}")]
    public async ValueTask<IActionResult> SetStatusAsync(long id, [FromBody] SetTaskStatusModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetTaskStatusAsync(id, model)
        });
    }
    [HttpPatch("set-priority/{id:long}")]
    public async ValueTask<IActionResult> SetPriorityAsync(long id, [FromBody] SetTaskPriorityModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetTaskPriorityAsync(id, model)
        });
    }
    [HttpPatch("set-reccuring/{id:long}")]
    public async ValueTask<IActionResult> SetReccuringAsync(long id, [FromBody] SetTaskReccuringModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetTaskReccuringAsync(id, model)
        });
    }
    [HttpPatch("set-reminder/{id:long}")]
    public async ValueTask<IActionResult> SetReminderAsync(long id, [FromBody] SetTaskReminderModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetTaskReminderAsync(id, model)
        });
    }
    [HttpPatch("set-duedate/{id:long}")]
    public async ValueTask<IActionResult> SetDueDateAsync(long id, [FromBody] SetTaskDueDateModel model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetTaskDueDateAsync(id, model)
        });
    }

    [HttpPatch("set-iscompleted/{id:long}")]
    public async ValueTask<IActionResult> SetIsCompletedAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.SetIsCompletedAsync(id)
        });
    }
    [HttpPatch("unset-iscompleted/{id:long}")]
    public async ValueTask<IActionResult> UnsetIsCompletedAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetIsCompletedAsync(id)
        });
    }

    [HttpPatch("unset-status/{id:long}")]
    public async ValueTask<IActionResult> UnsetStatusAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetTaskStatusAsync(id)
        });
    }
    [HttpPatch("unset-priority/{id:long}")]
    public async ValueTask<IActionResult> UnsetPriorityAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetTaskPriorityAsync(id)
        });
    }
    [HttpPatch("unset-reccuring/{id:long}")]
    public async ValueTask<IActionResult> UnsetReccuringAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetTaskReccuringAsync(id)
        });
    }
    [HttpPatch("unset-reminder/{id:long}")]
    public async ValueTask<IActionResult> UnsetReminderAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetTaskReminderAsync(id)
        });
    }
    [HttpPatch("unset-duedate/{id:long}")]
    public async ValueTask<IActionResult> UnsetDueDateAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.UnsetTaskDueDateAsync(id)
        });
    }


    [HttpGet("completed")]
    public async ValueTask<IActionResult> GetAllCompletedAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetAllCompletedTasksAsync(@params, filter)
        });
    }
    [HttpGet("not-completed")]
    public async ValueTask<IActionResult> GetAllNotCompletedAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetAllNotCompletedTasksAsync(@params, filter)
        });
    }


    [HttpGet("by-duedate")]
    public async ValueTask<IActionResult> GetByDueDateAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] DateTime dueDate)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetTasksByDueDateAsync(@params, filter, dueDate)
        });
    }
    [HttpGet("by-reminder")]
    public async ValueTask<IActionResult> GetByReminderAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] DateTime reminder)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetTasksByReminderAsync(@params, filter, reminder)
        });
    }
    [HttpGet("by-priority")]
    public async ValueTask<IActionResult> GetByPriorityAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string priority)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetTasksByPriorityAsync(@params, filter, priority)
        });
    }
    [HttpGet("by-reccuring")]
    public async ValueTask<IActionResult> GetByReccuringAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string reccuring)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetTasksByReccuringAsync(@params, filter, reccuring)
        });
    }
    [HttpGet("by-status")]
    public async ValueTask<IActionResult> GetByStatusAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string status)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskService.GetTasksByStatusAsync(@params, filter, status)
        });
    }
}

