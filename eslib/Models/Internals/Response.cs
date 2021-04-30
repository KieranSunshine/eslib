using System.Net;

namespace Eslib.Models.Internals
{
    public class Response<T> : IResponse<T>
    {
        public Response(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public Response(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public Response(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }
        public Response(HttpStatusCode statusCode, Error error)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public HttpStatusCode StatusCode { get; }
        public bool Success => (
            StatusCode == HttpStatusCode.OK ||
            StatusCode == HttpStatusCode.NotModified ||
            StatusCode == HttpStatusCode.NoContent);
        public string? Message { get; }
        public T? Data { get; }
        public Error? Error { get; }
    }

    public interface IResponse<T>
    {
        public HttpStatusCode StatusCode { get; }
        public bool Success { get; }
        public string? Message { get; }
        public T? Data { get; }
        public Error? Error { get; }
    }
}
