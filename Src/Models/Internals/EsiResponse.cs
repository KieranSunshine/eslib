using System.Net;
using Eslib.Models.Internals.Interfaces;

namespace Eslib.Models.Internals
{
    public class EsiResponse<T> : IResponse
    {
        public EsiResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public EsiResponse(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public EsiResponse(HttpStatusCode statusCode, T data)
        {
            StatusCode = statusCode;
            Data = data;
        }
        public EsiResponse(HttpStatusCode statusCode, Error error)
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
}
