using Microsoft.AspNetCore.Mvc;
using RAD_BackEnd.DTOs.Notes;
using RAD_BackEnd.Services.Services.Notes;
using RAD_BackEnd.WebApi.Models;

namespace RAD_BackEnd.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController(INoteService noteService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] NoteCreateModel note)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteService.CreateAsync(note)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] NoteUpdateModel note)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await noteService.UpdateAsync(id, note)
        });
    }
}


