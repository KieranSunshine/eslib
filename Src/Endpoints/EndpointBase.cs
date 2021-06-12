using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;

namespace Eslib.Endpoints
{
    public abstract class EndpointBase
    {
        protected readonly IDataService _dataService;
        protected readonly IAuthenticationService _authenticationService;
        protected readonly IResponseFactory _responseFactory;

        protected EndpointBase(
            IDataService dataService,
            IAuthenticationService authenticationService)
        {
            _dataService = dataService;
            _authenticationService = authenticationService;
            _responseFactory = new ResponseFactory();
        }

        protected EndpointBase(
            IDataService dataService,
            IAuthenticationService authenticationService,
            IResponseFactory responseFactory)
        {
            _dataService = dataService;
            _authenticationService = authenticationService;
            _responseFactory = responseFactory;
        }
    }
}