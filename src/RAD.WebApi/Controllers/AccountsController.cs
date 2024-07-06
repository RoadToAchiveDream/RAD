using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.WebApi.ApiServices.Accounts;
using RAD.WebApi.DTOs.Accounts;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(IAccountApiService accountApiService) : ControllerBase
{
    [HttpGet("login")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> LoginAsync([FromQuery] LoginModel loginModel)
    {

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.LoginAsync(loginModel)
        });
    }

    [HttpGet("send-code")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> SendCodeAsync([FromQuery] SendCodeModel sendCodeModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.SendCodeAsync(sendCodeModel)
        });
    }

    [HttpGet("confirm-code")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> ConfirmAsync([FromQuery] ConfirmCodeModel confirmCodeModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.ConfirmCodeAsync(confirmCodeModel)
        });
    }

    [HttpPatch("reset-password")]
    [AllowAnonymous]
    public async ValueTask<IActionResult> ResetPasswordAsync([FromQuery] ResetPasswordModel resetPasswordModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await accountApiService.ResetPasswordAsync(resetPasswordModel)
        });
    }
}
