using eslib.Helpers.Wrappers;
using eslib.Models;
using eslib.Services.Handlers;
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
        private readonly IResponseHandler _responseHandler;

        public DataService(IOptions<ApiOptions> options)
        {
            _options = options;
            _httpClient = new HttpClientWrapper();
            _responseHandler = new ResponseHandler();
        }

        public DataService(IOptions<ApiOptions> options, IHttpClientWrapper httpClient, IResponseHandler responseHandler)
        {
            _options = options;
            _httpClient = httpClient;
            _responseHandler = responseHandler;
        }

        public async Task<Response<T>> Get<T>(string url)
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            return _responseHandler.Parse<T>(response);
        }

        public async Task<Response<T>> Post<T>(string url, object data)
        {                         
            var httpContent = new StringContent(JsonSerializer.Serialize(data));

            var response = await _httpClient.PostAsync(url, httpContent).ConfigureAwait(false);

            return _responseHandler.Parse<T>(response);
        }

        public string GenerateUrl(params string[] parameters)
        {
            var url = $"{Constants.apiUrl}/{string.Join("/", parameters)}";

            return url;
        }
    }

    public interface IDataService
    {
        public Task<Response<T>> Get<T>(string url);

        public Task<Response<T>> Post<T>(string url, object data);

        public string GenerateUrl(params string[] parameters);        
    }
}
