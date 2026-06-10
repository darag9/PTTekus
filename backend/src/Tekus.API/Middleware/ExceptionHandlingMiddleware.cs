using System.Net;
using System.Text.Json;
using Tekus.Application.Exceptions;

namespace Tekus.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            Tekus.Application.Exceptions.ValidationException validationEx => (HttpStatusCode.BadRequest, JsonSerializer.Serialize(new { Errors = validationEx.Errors })),
            NotFoundException => (HttpStatusCode.NotFound, JsonSerializer.Serialize(new { Error = exception.Message })),
            _ => (HttpStatusCode.InternalServerError, JsonSerializer.Serialize(new { Error = "An internal server error occurred." }))
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(message);
    }
}
