using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        public MetaEndpoint(ApiOptions options) : base(options) { }

        public MetaEndpoint(IDataService dataService, IRequestFactory requestFactory)
            : base(dataService, requestFactory) { }

        public Response<string> Ping()
        {
            var request = _requestFactory.Create()
                .AddPaths("ping");

            return _dataService.Get<string>(request).Result;
        }
    }
}
