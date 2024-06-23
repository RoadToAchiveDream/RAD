using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.Users;
using RAD.WebApi.DTOs.Users;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(IUserApiService userApiService) : ControllerBase
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
            Data = await userApiService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.GetAsync(id)
        });
    }
    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] UserCreateModel user)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.PostAsync(user)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.DeleteAsync(id)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] UserUpdateModel user)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.PutAsync(id, user)
        });
    }

    [HttpPatch("change-password")]
    public async ValueTask<IActionResult> ChangePasswordAsync([FromQuery] UserChangePasswordModel changePasswordModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userApiService.ChangePasswordAsync(changePasswordModel)
        });
    }
}

