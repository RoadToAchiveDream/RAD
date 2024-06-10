using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.NoteCategories;
using RAD.WebApi.DTOs.NoteCategories;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NoteCategoriesController(INoteCategoryApiService noteCategoryApiService) : ControllerBase
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
            Data = await noteCategoryApiService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] NoteCategoryCreateModel noteCategory)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.PostAsync(noteCategory)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] NoteCategoryUpdateModel note)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.PutAsync(id, note)
        });
    }
    [HttpPost("add-note-to-category")]
    public async ValueTask<IActionResult> AddNoteToCategoryAsync(long categoryId, long noteId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.AddNoteToCategoryAsync(categoryId, noteId)
        });
    }
    [HttpPatch("remove-note-from-category")]
    public async ValueTask<IActionResult> RemoveNoteFromCategoryAsync(long categoryId, long noteId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.RemoveNoteFromCategoryAsync(categoryId, noteId)
        });
    }
    [HttpGet("get-by-name")]
    public async ValueTask<IActionResult> GetCategoryByNameAsync(string name)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteCategoryApiService.GetCategoryByNameAsync(name)
        });
    }
}
