using System.Net;

namespace Eslib.Models.Internals.Interfaces
{
    public interface IResponse
    {
        public HttpStatusCode StatusCode { get; }
        public bool Success { get; }
        public string? Message { get; }
        public Error? Error { get; }
    }
}