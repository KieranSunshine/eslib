using eslib.Services;

namespace eslib.Endpoints
{
    public class Alliance
    {
        private readonly IDataService _dataService;
        private readonly string endpoint = "alliances";

        public Alliance(IDataService dataService)
        {
            _dataService = dataService;
        }

        public int[] GetAllianceIds()
        {            
            var url = _dataService.GenerateUrl(endpoint);
            var result = _dataService.Fetch<int[]>(url).Result;

            return result.data;
        }
    }
}
