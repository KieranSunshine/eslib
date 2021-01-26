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
        public AllianceEndpoint(IDataService dataService, IRequestFactory requestFactory)
            : base(dataService, requestFactory) { }

        public Response<int[]> GetAllianceIds()
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint);

            return _dataService.Get<int[]>(request).Result;
        }

        public Response<Alliance> GetAlliance(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString());

            return _dataService.Get<Alliance>(request).Result;
        }

        public Response<int[]> GetAllianceCorporationIds(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "corporations");

            return _dataService.Get<int[]>(request).Result;
        }

        public Response<Icon> GetAllianceIcon(int allianceId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, allianceId.ToString(), "icons");

            return _dataService.Get<Icon>(request).Result;
        }
    }
}
