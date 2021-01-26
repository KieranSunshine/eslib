using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class AllianceEndpoint: EndpointBase
    {
        private const string endpoint = "alliances";

        public AllianceEndpoint(ApiOptions options) : base(options) { }
        public AllianceEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory) { }

        public Response<int[]> GetAllianceIds()
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint);

            var result = _dataService.Get<int[]>(request).Result;
            
            return _responseFactory.Create<int[]>(result);
        }

        public Response<Alliance> GetAlliance(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString());

            var result = _dataService.Get<Alliance>(request).Result;

            return _responseFactory.Create<Alliance>(result);
        }

        public Response<int[]> GetAllianceCorporationIds(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "corporations");

            var result = _dataService.Get<int[]>(request).Result;

            return _responseFactory.Create<int[]>(result);
        }

        public Response<Icon> GetAllianceIcon(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "icons");

            var result = _dataService.Get<Icon>(request).Result;

            return _responseFactory.Create<Icon>(result);
        }
    }
}
