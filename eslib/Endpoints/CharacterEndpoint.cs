using System.Threading.Tasks;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class CharacterEndpoint : EndpointBase
    {
        private const string endpoint = "characters";
        public CharacterEndpoint(ApiOptions options)
            : base(options) { }

        public CharacterEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory) { }

        public async Task<IResponse<Character>> GetCharacter(int characterId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, characterId.ToString());

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Character>(result);
        }

        public async Task<IResponse<AgentResearch[]>> GetAgentsResearch(int characterId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), "agents_research");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<AgentResearch[]>(result);
        }
    }
}