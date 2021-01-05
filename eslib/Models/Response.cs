using System;
using System.Collections.Generic;
using System.Text;

namespace eslib.Models
{
    public class Response<T>
    {
        public T data { get; set; }
        public string error { get; set; }
    }
}
