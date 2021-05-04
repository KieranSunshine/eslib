using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Eslib.Models.Internals;

namespace Eslib.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public EsiResponse<T> Create<T>(HttpResponseMessage responseMessage) where T : class
        {
            EsiResponse<T> response;

            // TODO: Convert method to async and await here.
            var status = responseMessage.StatusCode;
            var content = responseMessage.Content.ReadAsStringAsync().Result;

            response = status switch {
                HttpStatusCode.OK or
                HttpStatusCode.NotModified or
                HttpStatusCode.NoContent => ProcessResponse<T>(status, content),
                _                        => ProcessError<T>(status, content) 
            };

            return response;
        }

        private static EsiResponse<T> ProcessResponse<T>(HttpStatusCode statusCode, string content)
        {
            if (string.IsNullOrEmpty(content))
                return new EsiResponse<T>(statusCode);

            // If data type is string, do not deserialize...
            if (typeof(T) == typeof(string))
                return new EsiResponse<T>(statusCode, content);
            
            // Attempt to deserialize...
            T? data;
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
                throw new ConversionException("Converted data cannot be null");
            
            return new EsiResponse<T>(statusCode, data);
        }

        private static EsiResponse<T> ProcessError<T>(HttpStatusCode statusCode, string content)
        {
            // If no content received, return a generic response...
            if (string.IsNullOrEmpty(content))
            {
                return new EsiResponse<T>(statusCode);
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
            
            return new EsiResponse<T>(statusCode, error);
        }
    }
    
    public interface IResponseFactory
    {
        public EsiResponse<T> Create<T>(HttpResponseMessage responseMessage) where T : class;
    }
}
