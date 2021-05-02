using Eslib.Models.Internals;

namespace Eslib.Services.Factories
{
    public class RequestFactory : IRequestFactory
    {
        private readonly ApiOptions _options;

        public RequestFactory(ApiOptions options)
        {
            _options = options;
        }

        public Request Create()
        {
            var request = new Request
            {
                Url = $"{_options.ApiUrl}/{_options.Version}"
            };

            request
                .AddQuery("datasource", _options.DataSource);

            return request;
        }
    }
    
    public interface IRequestFactory
    {
        /// <summary>
        /// Creates an appropriately created Request
        /// </summary>
        /// <returns>The request</returns>
        public Request Create();
    }
}