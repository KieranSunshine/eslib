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
        private readonly IResponseFactory _responseFactory;

        public DataService()
        {
            _httpClient = new HttpClientWrapper();
            _responseFactory = new ResponseFactory();
        }

        public DataService(IHttpClientWrapper httpClient, IResponseFactory responseFactory)
        {
            _httpClient = httpClient;
            _responseFactory = responseFactory;
        }

        public async Task<Response<T>> Get<T>(Request request) where T: class
        {
            var response = await _httpClient
                .GetAsync(request.Url)
                .ConfigureAwait(false);

            return _responseFactory.CreateResponse<T>(response);
        }

        public async Task<Response<T>> Post<T>(Request request) where T: class
        {                         
            var httpContent = new StringContent(JsonSerializer.Serialize(request.Data));

            var response = await _httpClient
                .PostAsync(request.Url, httpContent)
                .ConfigureAwait(false);

            return _responseFactory.CreateResponse<T>(response);
        }
    }

    public interface IDataService
    {
        public Task<Response<T>> Get<T>(Request request) where T: class;
        public Task<Response<T>> Post<T>(Request request) where T: class;   
    }
}
