using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using eslib.Models.Internals;

namespace eslib.Services.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public IResponse<T> Create<T>(HttpResponseMessage responseMessage) where T : class
        {
            IResponse<T> response;

            // TODO: Convert method to async and await here.
            var content = responseMessage.Content.ReadAsStringAsync().Result;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.NotModified:
                    response = ProcessResponse<T>(responseMessage.StatusCode, content);
                    break;

                default:
                    response = ProcessError<T>(responseMessage.StatusCode, content);
                    break;
            }

            return response;
        }

        private IResponse<T> ProcessResponse<T>(HttpStatusCode statusCode, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new Response<T>(statusCode);
            }

            // If data type is string, do not deserialize...
            if (typeof(T) == typeof(string))
            {
                return new Response<T>(statusCode, content);
            }
            
            // Attempt to deserialize...
            T data;
            try
            {
                data = JsonSerializer.Deserialize<T>(content);
            }
            catch (Exception e)
            {
                throw new FormatException("Unable to deserialize the response into the given type", e);
            }

            // Double check for null value.
            if (data is null)
            {
                throw new ConversionException("Converted data cannot be null");
            }

            return new Response<T>(statusCode, data);
        }

        private IResponse<T> ProcessError<T>(HttpStatusCode statusCode, string content)
        {
            // If no content received, return a generic response...
            if (string.IsNullOrEmpty(content))
            {
                return new Response<T>(statusCode);
            }

            // Attempt to deserialize the error...
            Error? error;
            try
            {
                error = JsonSerializer.Deserialize<Error>(content);
            }
            catch (Exception e)
            {
                throw new FormatException("Unable to deserialize the response error", e);
            }

            // Double check for null value.
            if (error is null)
            {
                throw new ConversionException("Error data cannot be null");
            }
            
            return new Response<T>(statusCode, error);
        }
    }
    
    public interface IResponseFactory
    {
        public IResponse<T> Create<T>(HttpResponseMessage responseMessage) where T : class;
    }
}
