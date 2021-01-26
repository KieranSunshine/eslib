using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public abstract class EndpointBase
    {
        protected readonly IDataService _dataService;
        protected readonly IRequestFactory _requestFactory;
        protected readonly IResponseFactory _responseFactory;

        protected EndpointBase(ApiOptions options)
        {
            _dataService = new DataService();
            _requestFactory = new RequestFactory(options);
            _responseFactory = new ResponseFactory();
        }

        protected EndpointBase(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
        {
            _dataService = dataService;
            _requestFactory = requestFactory;
            _responseFactory = responseFactory;
        }
    }
}