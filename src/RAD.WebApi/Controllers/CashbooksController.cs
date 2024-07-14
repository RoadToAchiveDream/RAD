using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAD.Services.Configurations;
using RAD.WebApi.ApiServices.Cashbooks;
using RAD.WebApi.DTOs.Cashbooks;
using RAD.WebApi.Models;

namespace RAD.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CashbooksController(ICashbookApiService cashbookService) : ControllerBase
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
            Data = await cashbookService.GetAsync(@params, filter, search)
        });
    }
    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await cashbookService.GetAsync(id)
        });
    }
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync([FromBody] CashbookCreateModel cashbook)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await cashbookService.PostAsync(cashbook)
        });
    }
    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await cashbookService.DeleteAsync(id)
        });
    }
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, [FromBody] CashbookUpdateModel cashbook)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await cashbookService.PutAsync(id, cashbook)
        });
    }
}
