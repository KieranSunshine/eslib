using System.Net.Http;
using System.Threading.Tasks;
using Eslib.Helpers.Wrappers;
using Eslib.Models.Internals;
using Flurl;

namespace Eslib.Services
{
    public class DataService : IDataService
    {
        private ApiOptions _options;
        private readonly IHttpClientWrapper _httpClient;

        #region Constructors

        public DataService(ApiOptions options)
        {
            _options = options;
            _httpClient = new HttpClientWrapper();
        }

        public DataService(ApiOptions options, IHttpClientWrapper httpClient)
        {
            _options = options;
            _httpClient = httpClient;
        }

        #endregion

        public async Task<HttpResponseMessage> GetAsync(Url url)
        {
            url = ValidateUrl(url);
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = url.ToUri()
            };

            var response = await _httpClient
                .SendAsync(request)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(Url url)
        {
            url = ValidateUrl(url);
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = url.ToUri()
            };

            var response = await _httpClient
                .SendAsync(request)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(Url url)
        {
            url = ValidateUrl(url);
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = url.ToUri()
            };

            var response = await _httpClient
                .SendAsync(request)
                .ConfigureAwait(false);

            return response;
        }

        private Url ValidateUrl(Url url)
        {
            if (!url.QueryParams.Contains("datasource"))
                url.SetQueryParam("datasource", _options.DataSource);

            return url;
        }
    }

    public interface IDataService
    {
        public Task<HttpResponseMessage> GetAsync(Url url);
        public Task<HttpResponseMessage> PostAsync(Url url);
        public Task<HttpResponseMessage> PutAsync(Url url);
    }
}