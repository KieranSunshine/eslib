using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using eslib.Helpers.Wrappers;
using eslib.Models.Internals;
using eslib.Services.Factories;

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

        public async Task<HttpResponseMessage> Get<T>(Request request) where T: class
        {
            var response = await _httpClient
                .GetAsync(request.Url)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> Post<T>(Request request) where T: class
        {                         
            var httpContent = new StringContent(JsonSerializer.Serialize(request.Data));

            var response = await _httpClient
                .PostAsync(request.Url, httpContent)
                .ConfigureAwait(false);

            return response;
        }
    }

    public interface IDataService
    {
        public Task<HttpResponseMessage> Get<T>(Request request) where T: class;
        public Task<HttpResponseMessage> Post<T>(Request request) where T: class;   
    }
}
