using eslib.Helpers.Wrappers;
using eslib.Models;
using Microsoft.Extensions.Options;
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

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;

                if (result.Length > 0)
                {
                    try
                    {
                        // If result is valid json then set the data property.
                        response.data = JsonSerializer.Deserialize<T>(result);
                    }
                    catch (JsonException ex)
                    {
                        // If result was not valid Json, set the message property instead.
                        response.message = result;
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
