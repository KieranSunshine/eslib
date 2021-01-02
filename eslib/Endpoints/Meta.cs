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
            var endpoint = "ping";
            var url = $"{Constants.apiUrl}/{endpoint}/";

            var result = _dataService.Fetch<string>(url).Result;

            return result.message;
        }
    }
}
