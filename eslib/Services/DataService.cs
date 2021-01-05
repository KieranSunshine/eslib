using eslib.Helpers.Wrappers;
using eslib.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace eslib.Services
{
    public class DataService : IDataService
    {
        private readonly IOptions<ApiOptions> _options;
        private readonly IHttpClientWrapper _httpClient;

        public DataService(IOptions<ApiOptions> options)
        {
            _options = options;
            _httpClient = new HttpClientWrapper();
        }

        public DataService(IOptions<ApiOptions> options, IHttpClientWrapper httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task<Response<T>> Fetch<T>(string url)
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            return ParseResponse<T>(response);
        }

        public Response<T> ParseResponse<T>(HttpResponseMessage responseMessage)
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

        public string GenerateUrl(params string[] parameters)
        {
            var url = $"{Constants.apiUrl}/{string.Join("/", parameters)}";

            return url;
        }
    }

    public interface IDataService
    {
        public Task<Response<T>> Fetch<T>(string url);

        public Response<T> ParseResponse<T>(HttpResponseMessage responseMessage);

        public string GenerateUrl(params string[] parameters);        
    }
}
