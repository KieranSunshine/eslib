using System;
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
                .AddPaths(endpoint, characterId.ToString(), "agents_research");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<AgentResearch[]>(result);
        }

        public async Task<IResponse<Blueprint[]>> GetBlueprints(int characterId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, characterId.ToString(), "blueprints");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Blueprint[]>(result);
        }

        public async Task<IResponse<CorporationHistory[]>> GetCorporationHistory(int characterId)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, characterId.ToString(), "corporationhistory");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<CorporationHistory[]>(result);
        }
        
        public async Task<IResponse<double>> CalculateCspa(int characterId, int[] characters)
        {
            var request = _requestFactory.Create()
                .AddPaths(endpoint, characterId.ToString(), "cspa");

            if (characters.Length > 0 && characters.Length <= 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(characters), 
                    "length of array must be between 1 and 100 items");
            }
            request.Data = characters;

            var result = await _dataService.Post(request);

            return _responseFactory.Create<double>(result);
        }
    }
}