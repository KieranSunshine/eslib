using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public abstract class EndpointBase
    {
        protected readonly IDataService _dataService;

        protected EndpointBase(ApiOptions options)
        {
            _dataService = new DataService(options);
        }

        protected EndpointBase(IDataService dataService)
        {
            _dataService = dataService;
        }
    }
}