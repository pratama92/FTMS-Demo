using FTMS.API.Middleware;

namespace FTMS.API.Extensions;

public static class MiddlewareExtension
{
    public static WebApplication UseCustomMiddleware(
        this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}