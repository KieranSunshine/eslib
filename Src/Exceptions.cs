using System;

namespace Eslib
{
    public class ConversionException : ApplicationException
    {
        public ConversionException(string message)
            : base(message) { }

        public ConversionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}