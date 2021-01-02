using eslib.Services;

namespace eslib.Endpoints
{
    public class Meta
    {
        private readonly IDataService _dataService;

        public Meta(IDataService dataService)
        {
            _dataService = dataService;
        }

        public string Ping()
        {            
            var url = _dataService.GenerateUrl("ping");
            var result = _dataService.Fetch<string>(url).Result;

            return result.message;
        }
    }
}
