using System.Net.Http;
using System.Threading.Tasks;

namespace eslib.Helpers.Wrappers
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient _httpClient;

        static HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent)
        {
            return await _httpClient.PostAsync(url, httpContent);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }

    public interface IHttpClientWrapper
    {
        public Task<HttpResponseMessage> GetAsync(string url);

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent httpContent);
    }
}