using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eslib.Helpers.Wrappers;
using eslib.Models.Internals;

namespace eslib.Services
{
    public class DataService : IDataService
    {
        private readonly IHttpClientWrapper _httpClient;

        public DataService()
        {
            _httpClient = new HttpClientWrapper();
        }

        public DataService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Get(Request request)
        {
            var response = await _httpClient
                .GetAsync(request.Url)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> Post(Request request)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(request.Data));

            var response = await _httpClient
                .PostAsync(request.Url, httpContent)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> Put(Request request)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(request.Data));

            var response = await _httpClient
                .PutAsync(request.Url, httpContent)
                .ConfigureAwait(false);

            return response;
        }
    }

    public interface IDataService
    {
        public Task<HttpResponseMessage> Get(Request request);
        public Task<HttpResponseMessage> Post(Request request);
        public Task<HttpResponseMessage> Put(Request request);
    }
}