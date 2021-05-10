using Eslib.Endpoints;
using Eslib.Models.Internals;
using Microsoft.Extensions.Options;

namespace Eslib
{
    #pragma warning disable CS8618
    public class Esi
    {
        private ApiOptions _options;
        private EsiTokens _tokens = new();
        
        public Esi(IOptions<ApiOptions> apiOptions)
        {
            _options = apiOptions.Value;
        }

        public Esi(ApiOptions apiOptions)
        {
            _options = apiOptions;
        }
        
        public EsiTokens Tokens => _tokens;

        #region Endpoints

        public MetaEndpoint Meta => new(_options);
        public AllianceEndpoint Alliance => new(_options);
        public AssetsEndpoint Assets => new(_options);
        public BookmarksEndpoint Bookmarks => new(_options);
        public CalendarEndpoint Calendar => new(_options);

        #endregion
    }
    #pragma warning restore CS8618
}
