using eslib.Helpers.Wrappers;
using eslib.Models.Internals;
using eslib.Services.Handlers;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        public async Task<Response<T>> Get<T>(string url) where T: class
        {
            var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

            return _responseHandler.Parse<T>(response);
        }

        public async Task<Response<T>> Post<T>(string url, object data) where T: class
        {                         
            var httpContent = new StringContent(JsonSerializer.Serialize(data));

            var response = await _httpClient.PostAsync(url, httpContent).ConfigureAwait(false);

            return _responseHandler.Parse<T>(response);
        }

        public string GenerateUrl(params string[] pathArray)
        {
            var paths = new List<string>(); 
            foreach (var path in pathArray)
            {
                var encoded = Uri.EscapeDataString(path);

                paths.Add(encoded);
            }

            return $"{Constants.ApiUrl}/{string.Join("/", paths)}";
        }

        public string GenerateQueryString(IDictionary<string, string> parameters)
        {
            var queries = new List<string>();
            foreach (var param in parameters)
            {
                var key = Uri.EscapeDataString(param.Key);
                var value = Uri.EscapeDataString(param.Value);

                queries.Add($"{key}=${value}");
            }

            return string.Join("&", queries);
        }
    }

    public interface IDataService
    {
        public Task<Response<T>> Get<T>(string url) where T: class;

        public Task<Response<T>> Post<T>(string url, object data) where T: class;

        public string GenerateUrl(params string[] pathArray);  

        public string GenerateQueryString(IDictionary<string, string> parameters);      
    }
}
