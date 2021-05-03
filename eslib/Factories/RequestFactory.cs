using Eslib.Models.Internals;

namespace Eslib.Factories
{
    public class RequestFactory : IRequestFactory
    {
        private readonly ApiOptions _options;

        public RequestFactory(ApiOptions options)
        {
            _options = options;
        }

        public EsiRequest Create()
        {
            var request = new EsiRequest
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
        /// Creates an appropriately created EsiRequest
        /// </summary>
        /// <returns>The request</returns>
        public EsiRequest Create();
    }
}