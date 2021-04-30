using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Services.Factories;

namespace Eslib.Endpoints
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