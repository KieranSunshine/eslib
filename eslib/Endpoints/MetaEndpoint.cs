using System.Threading.Tasks;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;

namespace Eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        public MetaEndpoint(ApiOptions options) : base(options) { }

        public MetaEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory) { }

        public async Task<EsiResponse<string>> Ping()
        {
            var request = _requestFactory.Create()
                .AddPaths("ping");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<string>(result);
        }
    }
}
