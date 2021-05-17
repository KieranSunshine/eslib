using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;

namespace Eslib.Endpoints
{
    public abstract class EndpointBase
    {
        protected ApiOptions _options;
        protected readonly IDataService _dataService;
        protected readonly IResponseFactory _responseFactory;

        protected string _baseUrl => $"{_options.ApiUrl}/{_options.Version}";

        protected EndpointBase(ApiOptions options)
        {
            _options = options;
            _dataService = new DataService(options);
            _responseFactory = new ResponseFactory();
        }

        protected EndpointBase(
            IDataService dataService,
            IResponseFactory responseFactory)
        {
            _options = new ApiOptions();
            _dataService = dataService;
            _responseFactory = responseFactory;
        }
    }
}