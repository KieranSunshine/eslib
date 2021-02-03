using System.Threading.Tasks;
using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
{
    public class CharacterEndpoint : EndpointBase
    {
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
                .AddPaths("characters", characterId.ToString());

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Character>(result);
        }
    }
}