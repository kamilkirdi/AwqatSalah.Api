using System.Net;

namespace DiyanetNamazVakti.Api.WebCommon.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        string message = ex.Message;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(ValidationException))
        {
            statusCode = HttpStatusCode.NotAcceptable;
        }
        else if (exceptionType == typeof(InvalidOperationException))
        {
            statusCode = HttpStatusCode.NotFound;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        else if (exceptionType == typeof(BadHttpRequestException))
        {
            statusCode = HttpStatusCode.Unauthorized;
        }
        else
        {
            statusCode = HttpStatusCode.BadRequest;
            message = Dictionary.SomethingWentWrong;
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        }.ToString());
    }
}
