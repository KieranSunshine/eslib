using System.Diagnostics.CodeAnalysis;

namespace eslib.Models.Internals
{
    public class Response<T> where T: class
    {
        public T? Data { get; set; }
        public Error? Error { get; set; }
    }
}
