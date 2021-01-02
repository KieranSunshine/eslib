using eslib.Helpers.Wrappers;
using eslib.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
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

                response.message = result;
            }

            return response;
        }

        public string GenerateUrl(string endpoint)
        {
            return $"{Constants.apiUrl}/{endpoint}";
        }
    }

    public interface IDataService
    {
        public Task<Response<T>> Fetch<T>(string url);

        public Response<T> ParseResponse<T>(HttpResponseMessage responseMessage);

        public string GenerateUrl(string endpoint);        
    }
}
