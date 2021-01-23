using eslib.Models.Internals;
using eslib.Services;

namespace eslib.Endpoints
{
    public class MetaEndpoint
    {
        private readonly IDataService _dataService;

        public MetaEndpoint(IDataService dataService)
        {
            _dataService = dataService;
        }

        public Response<string> Ping()
        {            
            var url = _dataService.GenerateUrl("ping");
            var result = _dataService.Get<string>(url).Result;

            return result;
        }
    }
}
