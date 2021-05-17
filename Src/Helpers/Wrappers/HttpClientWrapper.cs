using System.Net.Http;
using System.Threading.Tasks;

namespace Eslib.Helpers.Wrappers
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient _httpClient;

        static HttpClientWrapper()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request);
        }
    }

    public interface IHttpClientWrapper
    {
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}