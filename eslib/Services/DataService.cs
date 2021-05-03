using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Helpers.Wrappers;
using Eslib.Models.Internals;

namespace Eslib.Services
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

        public async Task<HttpResponseMessage> Get(EsiRequest esiRequest)
        {
            var response = await _httpClient
                .GetAsync(esiRequest.Url)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> Post(EsiRequest esiRequest)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(esiRequest.Data));

            var response = await _httpClient
                .PostAsync(esiRequest.Url, httpContent)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> Put(EsiRequest esiRequest)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(esiRequest.Data));

            var response = await _httpClient
                .PutAsync(esiRequest.Url, httpContent)
                .ConfigureAwait(false);

            return response;
        }
    }

    public interface IDataService
    {
        public Task<HttpResponseMessage> Get(EsiRequest esiRequest);
        public Task<HttpResponseMessage> Post(EsiRequest esiRequest);
        public Task<HttpResponseMessage> Put(EsiRequest esiRequest);
    }
}