using System.Threading.Tasks;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        #region Constructors

        public MetaEndpoint(ApiOptions options) : base(options) { }

        public MetaEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory) { }

        #endregion

        public async Task<EsiResponse<string>> Ping()
        {
            var url = new Url(_baseUrl)
                .AppendPathSegment("ping");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<string>(response);
        }
    }
}
