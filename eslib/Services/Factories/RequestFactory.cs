using eslib.Models.Internals;

namespace eslib.Services.Factories
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
            var request = new Request()
                .AddPaths(_options.Version)
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