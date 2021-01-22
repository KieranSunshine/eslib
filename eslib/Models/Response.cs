using System.Diagnostics.CodeAnalysis;

namespace eslib.Models
{
    public class Response<T> where T: class
    {
        public T? Data { get; set; }
        public Error? Error { get; set; }
    }
}
