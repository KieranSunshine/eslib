using Eslib.Endpoints;
using Eslib.Models.Internals;
using Eslib.Services;
using Microsoft.Extensions.Options;

namespace Eslib
{
    #pragma warning disable CS8618
    public class Esi
    {
        private readonly EsiTokens _tokens = new();
        private readonly IDataService _dataService;
        private readonly IAuthenticationService _authenticationService;
        
        private ApiOptions _options;
        
        #region Constructors

        public Esi(IOptions<ApiOptions> apiOptions)
        {
            _options = apiOptions.Value;
            
            _dataService = new DataService(_options);
            _authenticationService = new AuthenticationService();
        }

        public Esi(ApiOptions apiOptions)
        {
            _options = apiOptions;

            _dataService = new DataService(_options);
            _authenticationService = new AuthenticationService();
        }

        #endregion
        
        public EsiTokens Tokens => _tokens;
        public IAuthenticationService Authentication => _authenticationService;

        #region Endpoints

        public MetaEndpoint Meta => new(_dataService, _authenticationService);
        public AllianceEndpoint Alliance => new(_dataService, _authenticationService);
        public AssetsEndpoint Assets => new(_dataService, _authenticationService);
        public BookmarksEndpoint Bookmarks => new(_dataService, _authenticationService);
        public CalendarEndpoint Calendar => new(_dataService, _authenticationService);

        #endregion
    }
    #pragma warning restore CS8618
}
