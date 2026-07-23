using FTMS.Application.Common;
using FTMS.Domain.Shared;
using System.Net;

namespace FTMS.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Success = false,
                Message = ex.Message,
                Code = ex.Code
            });
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Success = false,
                Message = ex.Message,
                Code = ex.Code
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Success = false,
                Message = "An unexpected error occurred."
            });
        }
    }
}