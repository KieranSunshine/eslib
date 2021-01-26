using System;
using System.Net.Http;
using System.Text.Json;
using eslib.Models.Internals;

namespace eslib.Services.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public Response<T> Create<T>(HttpResponseMessage responseMessage) where T: class
        {
            var response = new Response<T>();
            var result = responseMessage.Content.ReadAsStringAsync().Result;

            if (result.Length > 0)
            {
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // If a plain string is expected then it may not be JSONified...
                    if (typeof(T) == typeof(string))
                    {
                        response.Data = (T)Convert.ChangeType(result, typeof(T));
                    }
                    else
                    {
                        try
                        {
                            // If result is valid json then set the data property.
                            response.Data = JsonSerializer.Deserialize<T>(result);
                        }
                        catch (JsonException)
                        {
                            response.Error = new Error("Error parsing response into the given type");
                        }
                    }
                }
                else
                {
                    try
                    {
                        response.Error = JsonSerializer.Deserialize<Error>(result);
                    }
                    catch (JsonException)
                    {
                        // Something has gone rather wrong...
                        response.Error = new Error("Error parsing response");

                        throw;
                    }
                }
            }
            else
            {
                response.Error = new Error("No data received");
            }

            return response;
        }
    }

    public interface IResponseFactory
    {
        public Response<T> Create<T>(HttpResponseMessage responseMessage) where T: class;        
    }
}
