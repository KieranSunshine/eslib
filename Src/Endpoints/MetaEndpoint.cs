using System.Threading.Tasks;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class MetaEndpoint : EndpointBase
    {
        #region Constructors

        public MetaEndpoint(IDataService dataService, IAuthenticationService authenticationService)
            : base(dataService, authenticationService)
        {
        }

        public MetaEndpoint(
            IDataService dataService,
            IAuthenticationService authenticationService,
            IResponseFactory responseFactory)
            : base(dataService, authenticationService, responseFactory)
        {
        }

        #endregion
        
        public async Task<EsiResponse<string>> Ping()
        {
            var url = new Url()
                .AppendPathSegment("ping");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<string>(response);
        }
    }
}
