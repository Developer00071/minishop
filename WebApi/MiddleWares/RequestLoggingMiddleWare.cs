namespace WebApi.MiddleWares;

public class RequestLoggingMiddleWare
{
    private readonly RequestDelegate _request;
    public RequestLoggingMiddleWare(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        System.Console.WriteLine($"Request: {context.Request.Path}");
        await _request(context);
        System.Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}
