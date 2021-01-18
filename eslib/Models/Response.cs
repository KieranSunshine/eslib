namespace eslib.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public Error Error { get; set; }
    }
}
