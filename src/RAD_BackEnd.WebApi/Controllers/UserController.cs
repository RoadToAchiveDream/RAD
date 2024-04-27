using Microsoft.AspNetCore.Mvc;
using RAD_BackEnd.DTOs.Users;
using RAD_BackEnd.Services.Services.Users;
using RAD_BackEnd.WebApi.Models;

namespace RAD_BackEnd.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAllAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetAllAsync()
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetByIdAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserCreateModel user)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.CreateAsync(user)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] UserUpdateModel user)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.UpdateAsync(id, user)
        });
    }
}
