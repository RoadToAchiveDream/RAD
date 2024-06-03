using Microsoft.AspNetCore.Diagnostics;
using RAD_BackEnd.Services.Exceptions;
using RAD_BackEnd.WebApi.Models;

namespace RAD_BackEnd.WebApi.Middlewares;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        if (exception is not NotFoundException notFoundException)
            return false;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = notFoundException.StatusCode,
            Message = notFoundException.Message,
        });

        return true;
    }
}