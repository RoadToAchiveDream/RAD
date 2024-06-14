using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.TaskCategories;
using RAD.WebApi.DTOs.TaskCategories;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskCategoriesController(ITaskCategoryApiService taskCategoryApiService) : ControllerBase
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
            Data = await taskCategoryApiService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] TaskCategoryCreateModel taskCategory)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.PostAsync(taskCategory)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] TaskCategoryUpdateModel task)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.PutAsync(id, task)
        });
    }
    [HttpPost("add-task-to-category")]
    public async ValueTask<IActionResult> AddTaskToCategoryAsync(long categoryId, long taskId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.AddTaskToCategoryAsync(categoryId, taskId)
        });
    }
    [HttpPatch("remove-task-from-category")]
    public async ValueTask<IActionResult> RemoveTaskFromCategoryAsync(long categoryId, long taskId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.RemoveTaskFromCategoryAsync(categoryId, taskId)
        });
    }
    [HttpGet("get-by-name")]
    public async ValueTask<IActionResult> GetCategoryByNameAsync(string name)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await taskCategoryApiService.GetCategoryByNameAsync(name)
        });
    }
}
