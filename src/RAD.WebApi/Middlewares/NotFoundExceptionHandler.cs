using Microsoft.AspNetCore.Diagnostics;
using RAD.Services.Exceptions;
using RAD.WebApi.Models;

namespace RAD.WebApi.Middlewares;

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