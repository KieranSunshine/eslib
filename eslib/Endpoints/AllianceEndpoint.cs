using eslib.Models;
using eslib.Services;

namespace eslib.Endpoints
{
    public class AllianceEndpoint
    {
        private readonly IDataService _dataService;
        private readonly string endpoint = "alliances";

        public AllianceEndpoint(IDataService dataService)
        {
            _dataService = dataService;
        }

        public int[] GetAllianceIds()
        {            
            var url = _dataService.GenerateUrl(endpoint);
            var result = _dataService.Fetch<int[]>(url).Result;

            return result.data;
        }

        public Alliance GetAlliance(int allianceId)
        {
            var url = _dataService.GenerateUrl(endpoint, allianceId.ToString());
            var result = _dataService.Fetch<Alliance>(url).Result;

            return result.data;
        }

        public int[] GetAllianceCorporationIds(int allianceId)
        {
            var url = _dataService.GenerateUrl(endpoint, allianceId.ToString());
            var result = _dataService.Fetch<int[]>(url).Result;

            return result.data;
        }
    }
}
