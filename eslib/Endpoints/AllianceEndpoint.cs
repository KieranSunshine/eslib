using System.Threading.Tasks;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class AllianceEndpoint : EndpointBase
    {
        private const string endpoint = "alliances";

        public AllianceEndpoint(ApiOptions options) : base(options)
        {
        }

        public AllianceEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory)
        {
        }

        public async Task<IResponse<int[]>> GetAllianceIds()
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint);

            var result = await _dataService.Get(request);

            return _responseFactory.Create<int[]>(result);
        }

        public async Task<IResponse<Alliance>> GetAlliance(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString());

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Alliance>(result);
        }

        public async Task<IResponse<int[]>> GetAllianceCorporationIds(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "corporations");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<int[]>(result);
        }

        public async Task<IResponse<Icon>> GetAllianceIcon(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "icons");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Icon>(result);
        }
    }
}