﻿using eslib.Models;
using System;
using System.Net.Http;
using System.Text.Json;

namespace eslib.Services.Handlers
{
    public class ResponseHandler : IResponseHandler
    {
        public Response<T> Parse<T>(HttpResponseMessage responseMessage)
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
                        response.data = (T)Convert.ChangeType(result, typeof(T));
                    }
                    else
                    {
                        try
                        {
                            // If result is valid json then set the data property.
                            response.data = JsonSerializer.Deserialize<T>(result);
                        }
                        catch (JsonException)
                        {
                            response.error = "Error parsing response into the given type";
                        }
                    }
                }
                else
                {
                    try
                    {
                        response.error = JsonSerializer.Deserialize<Error>(result).Message;
                    }
                    catch (JsonException)
                    {
                        // Something has gone rather wrong...
                        response.error = "Error parsing response";

                        throw;
                    }
                }
            }

            return response;
        }
    }

    public interface IResponseHandler
    {
        public Response<T> Parse<T>(HttpResponseMessage responseMessage);        
    }
}