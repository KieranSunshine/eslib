using eslib.Models.Internals;
using eslib.Services;

namespace eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        public MetaEndpoint(ApiOptions options) : base(options) { }

        public MetaEndpoint(IDataService dataService) : base(dataService) { }

        public Response<string> Ping()
        {            
            var url = _dataService.GenerateUrl("ping");
            var result = _dataService.Get<string>(url).Result;

            return result;
        }
    }
}
