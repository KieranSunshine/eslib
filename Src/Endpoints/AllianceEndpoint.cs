using System.Threading.Tasks;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class AllianceEndpoint : EndpointBase
    {
        #region Constructors

        public AllianceEndpoint(IDataService dataService, IAuthenticationService authenticationService) 
            : base(dataService, authenticationService)
        {
        }

        public AllianceEndpoint(
            IDataService dataService,
            IAuthenticationService authenticationService,
            IResponseFactory responseFactory)
            : base(dataService, authenticationService, responseFactory)
        {
        }
        
        #endregion

        public async Task<EsiResponse<int[]>> GetAllianceIds()
        {
            var url = new Url()
                .AppendPathSegment("alliances");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<int[]>(response);
        }

        public async Task<EsiResponse<Alliance>> GetAlliance(int allianceId)
        {
            var url = new Url()
                .AppendPathSegments("alliances", allianceId);

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<Alliance>(response);
        }

        public async Task<EsiResponse<int[]>> GetAllianceCorporationIds(int allianceId)
        {
            var url = new Url()
                .AppendPathSegments("alliances", allianceId, "corporations");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<int[]>(response);
        }

        public async Task<EsiResponse<Icon>> GetAllianceIcon(int allianceId)
        {
            var url = new Url()
                .AppendPathSegments("alliances", allianceId, "icons");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<Icon>(response);
        }
    }
}