using System.Net;

namespace Applicatiion.Responses;

public sealed class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
    public T? Data { get; }

    public ApiResponse(HttpStatusCode statusCode, string message, T? data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public ApiResponse(HttpStatusCode statusCode, string message)
        : this(statusCode, message, default)
    {
    }
}
